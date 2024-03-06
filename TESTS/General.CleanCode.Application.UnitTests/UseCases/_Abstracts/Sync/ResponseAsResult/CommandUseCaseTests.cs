using Xunit;


using General.CleanCode.Domain.ErrorHandling;
using General.CleanCode.Application.CleanArchi.UseCases;


namespace General.CleanCode.Application.UnitTests.UseCases;

public class CommandUseCaseTests
{
    #region RunAsync
    [Fact]
    public async Task RunAsync_WhenResultIsSuccess_ShouldReturnATaskOfSuccessfulResult()
    {
        //--- Arrange ---
        MyCommandUseCase useCase = new();
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
        MyCommandUseCase useCase = new();
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
        MyCommandUseCase3 useCase = new();
        long invalidId = -1;
        MyUseCaseRequest request = new(SomePositiveId: invalidId);

        CancellationTokenSource cts = new();
        CancellationToken token = cts.Token;


        //--- Act && Assert ---
        Task<MyCommandUseCase3.InvalidIdException> tskEx =
            Assert.ThrowsAsync<MyCommandUseCase3.InvalidIdException>(() => useCase.RunAsync(request, token));

        MyCommandUseCase3.InvalidIdException ex = await tskEx;

        var expectedMessage = string.Format(MyCommandUseCase3.InvalidIdException.MESSAGE_FORMAT, invalidId);
        Assert.Equal(expectedMessage, ex.Message);
    }

    #region CancellationToken
    [Fact]
    public async Task RunAsync_WhenResultIsFailureDueToCancellationTokenActivatitedBeforeCall_ShouldReturnATaskOfFailedResultWithTheCorrectError()
    {
        //--- Arrange ---
        MyCommandUseCase useCase = new();
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
        MyCommandUseCase2 useCase = new();
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
    #endregion CancellationToken
    #endregion RunAsync


    #region Run
    [Fact]
    public void Run_WhenResultIsSuccess_ShouldReturnATaskOfSuccessfulResult()
    {
        //--- Arrange ---
        MyCommandUseCase useCase = new();
        MyUseCaseRequest request = new(SomePositiveId: 0);


        //--- Act ---
        Result result = useCase.Run(request);

        //--- Assert ---
        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Null(result.Error);
    }

    [Fact]
    public void Run_WhenResultIsFailureAndReturnsAResult_ShouldReturnATaskOfFailedResultWithTheCorrectError()
    {
        //--- Arrange ---
        MyCommandUseCase useCase = new();
        long invalidId = -1;
        MyUseCaseRequest request = new(SomePositiveId: invalidId);


        //--- Act ---
        Result result = useCase.Run(request);

        //--- Assert ---
        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Equal(Errors.InvalidIdError, result.Error);
    }

    [Fact]
    public void Run_WhenResultIsFailureButThrowsAnExceptionInsteadOfAResult_ShouldPropagateThisExceptionUpToTheCaller()
    {
        //--- Arrange ---
        MyCommandUseCase3 useCase = new();
        long invalidId = -1;
        MyUseCaseRequest request = new(SomePositiveId: invalidId);


        //--- Act && Assert ---
        MyCommandUseCase3.InvalidIdException ex = Assert.Throws<MyCommandUseCase3.InvalidIdException>(() => useCase.Run(request));

        var expectedMessage = string.Format(MyCommandUseCase3.InvalidIdException.MESSAGE_FORMAT, invalidId);
        Assert.Equal(expectedMessage, ex.Message);
    }
    #endregion Run
}

class MyCommandUseCase : CommandUseCase<MyUseCaseRequest>
{
    protected override Result Handle(MyUseCaseRequest request)
    {
        Result result = (request.SomePositiveId >= 0) ? Result.Ok() : Result.NotOk(Errors.InvalidIdError);
        return result;
    }
}

class MyCommandUseCase2 : CommandUseCase<MyUseCaseRequest>
{
    protected override Result Handle(MyUseCaseRequest request)
    {
        if (request.SomePositiveId < 0)
        {
            throw new TaskCanceledException();
        }

        return Result.Ok();
    }
}


class MyCommandUseCase3 : CommandUseCase<MyUseCaseRequest>
{
    protected override Result Handle(MyUseCaseRequest request)
    {
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