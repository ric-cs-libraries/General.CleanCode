using Xunit;


using General.Basics.ErrorHandling;


using General.CleanCode.CleanArchi.Application.UseCases;


namespace General.CleanCode.CleanArchi.Application.UnitTests.UseCases;

public class CommandUseCaseAsyncTests
{
    #region RunAsync
    [Fact]
    public async Task RunAsync_WhenResultIsSuccess_ShouldReturnATaskOfSuccessfulResult()
    {
        //--- Arrange ---
        MyCommandUseCaseAsync useCase = new();
        MyUseCaseRequest request = new(SomePositiveId: 0);

        CancellationTokenSource cts = new();
        CancellationToken token = cts.Token;


        //--- Act ---
        Result result = await useCase.RunAsync(request, token);

        //--- Assert ---
        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Null(result.Error);
    }

    [Fact]
    public async Task RunAsync_WhenResultIsFailureAndReturnsAResult_ShouldReturnATaskOfFailedResultWithTheCorrectError()
    {
        //--- Arrange ---
        MyCommandUseCaseAsync useCase = new();
        long invalidId = -1;
        MyUseCaseRequest request = new(SomePositiveId: invalidId);

        CancellationTokenSource cts = new();
        CancellationToken token = cts.Token;


        //--- Act ---
        Result result = await useCase.RunAsync(request, token);

        //--- Assert ---
        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Equal(Errors.InvalidIdError, result.Error);
    }

    [Fact]
    public async Task RunAsync_WhenResultIsFailureButThrowsAnExceptionInsteadOfAResult_ShouldPropagateThisExceptionUpToTheCaller()
    {
        //--- Arrange ---
        MyCommandUseCaseAsync3 useCase = new();
        long invalidId = -1;
        MyUseCaseRequest request = new(SomePositiveId: invalidId);

        CancellationTokenSource cts = new();
        CancellationToken token = cts.Token;


        //--- Act && Assert ---
        Task<MyCommandUseCaseAsync3.InvalidIdException> tskEx =
            Assert.ThrowsAsync<MyCommandUseCaseAsync3.InvalidIdException>(() => useCase.RunAsync(request, token));
        
        MyCommandUseCaseAsync3.InvalidIdException ex = await tskEx;

        var expectedMessage = string.Format(MyCommandUseCaseAsync3.InvalidIdException.MESSAGE_FORMAT, invalidId);
        Assert.Equal(expectedMessage, ex.Message);
    }
    #endregion RunAsync

    #region RunAsync & CancellationToken
    [Fact]
    public async Task RunAsync_WhenResultIsFailureDueToCancellationTokenActivatitedBeforeCall_ShouldReturnATaskOfFailedResultWithTheCorrectError()
    {
        //--- Arrange ---
        MyCommandUseCaseAsync useCase = new();
        MyUseCaseRequest request = new(SomePositiveId: 0);

        CancellationTokenSource cts = new();
        CancellationToken token = cts.Token;
        cts.Cancel(); //<<<<<<<<<<<<


        //--- Act ---
        Result result = await useCase.RunAsync(request, token);

        //--- Assert ---
        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Equal(AsyncErrors.UseCaseHandlingTaskOperationCancellation, result.Error);
    }

    [Fact]
    public async Task RunAsync_WhenResultIsFailureDueToTaskCanceledExceptionThrownFromInsideTheUseCase_ShouldReturnATaskOfFailedResultWithTheCorrectError()
    {
        //--- Arrange ---
        MyCommandUseCaseAsync2 useCase = new();
        MyUseCaseRequest request = new(SomePositiveId: -1);

        CancellationTokenSource cts = new();
        CancellationToken token = cts.Token;


        //--- Act ---
        Result result = await useCase.RunAsync(request, token);

        //--- Assert ---
        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Equal(AsyncErrors.UseCaseHandlingTaskCancellation, result.Error);
    }
    #endregion RunAsync & CancellationToken
}

class MyCommandUseCaseAsync : CommandUseCaseAsync<MyUseCaseRequest>
{
    protected override async Task<Result> HandleAsync(MyUseCaseRequest request, CancellationToken token)
    {
        TaskCompletionSource<Result> tcs = new();

        Result result = (request.SomePositiveId >= 0) ? Result.Ok() : Result.NotOk(Errors.InvalidIdError);

        tcs.SetResult(result);

        return await tcs.Task;
        //return await Task.FromResult(result);
    }
}


class MyCommandUseCaseAsync2 : CommandUseCaseAsync<MyUseCaseRequest>
{
    protected override async Task<Result> HandleAsync(MyUseCaseRequest request, CancellationToken token)
    {
        await Task.Delay(TimeSpan.FromMicroseconds(2));


        if (request.SomePositiveId < 0)
        {
            throw new TaskCanceledException();
        }

        return Result.Ok();
    }
}


class MyCommandUseCaseAsync3 : CommandUseCaseAsync<MyUseCaseRequest>
{
    protected override async Task<Result> HandleAsync(MyUseCaseRequest request, CancellationToken token)
    {
        await Task.Delay(TimeSpan.FromMicroseconds(2));


        if (request.SomePositiveId < 0)
        {
            throw new InvalidIdException(request.SomePositiveId);
        }

        return Result.Ok();
    }

    public class InvalidIdException : Exception
    {
        public const string MESSAGE_FORMAT = "The Id={0}, is invalid, should be >=0.";

        public override string Message { get; }

        public InvalidIdException(long Id)
        {
            Message = string.Format(MESSAGE_FORMAT, Id);
        }
    }
}