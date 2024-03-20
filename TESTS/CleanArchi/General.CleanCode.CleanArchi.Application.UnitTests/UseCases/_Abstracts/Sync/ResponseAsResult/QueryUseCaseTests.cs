using Xunit;
using Moq;


using General.Basics.ErrorHandling;

using General.CleanCode.CleanArchi.Application.UseCases;


namespace General.CleanCode.CleanArchi.Application.UnitTests.UseCases;

public class QueryUseCaseTests
{
    Mock<ISomeService> someServiceMock;

    public QueryUseCaseTests()
    {
        someServiceMock = new();
    }

    #region RunAsync
    [Fact]
    public async Task RunAsync_WhenResultIsSuccess_ShouldReturnATaskOfSuccessfulResultWithTheCorrectResponseValue()
    {
        //--- Arrange ---
        FindDataDtoResponse serviceResponse = new(10, "SomeString");
        someServiceMock.Setup(srv => srv.FindData(It.IsAny<long>())).Returns(serviceResponse);

        MyQueryUseCase useCase = new(someServiceMock.Object);
        long anyId = 5;
        MyUseCaseRequest request = new(SomePositiveId: anyId);
        MyUseCaseResponse expectedResponse = new(SomeIntValue: serviceResponse.IntValue, SomeStringValue: serviceResponse.StringValue);

        CancellationTokenSource cts = new();
        CancellationToken token = cts.Token;


        //--- Act ---
        Result<MyUseCaseResponse> result = await useCase.RunAsync(request, token);

        //--- Assert ---
        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Null(result.Error);
        Assert.Equal(expectedResponse, result.Value);
    }

    [Fact]
    public async Task RunAsync_WhenResultIsFailureAndReturnsAResult_ShouldReturnATaskOfFailedResultWithTheCorrectError()
    {
        //--- Arrange ---
        long unexistingId = 10;
        someServiceMock.Setup(srv => srv.FindData(unexistingId)).Throws(new ISomeService.UnfoundIdException(unexistingId));
        string expectedExceptionMessage = string.Format(ISomeService.UnfoundIdException.MESSAGE_FORMAT, unexistingId);

        MyQueryUseCase useCase = new(someServiceMock.Object);
        MyUseCaseRequest request = new(SomePositiveId: unexistingId);

        CancellationTokenSource cts = new();
        CancellationToken token = cts.Token;


        //--- Act ---
        Result<MyUseCaseResponse> result = await useCase.RunAsync(request, token);

        //--- Assert ---
        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Equal(new Error(MyQueryUseCase.UNFOUND_ID_ERROR_CODE, expectedExceptionMessage), result.Error);


        var ex = Assert.Throws<UnavailableResultValueException>(() => result.Value);
        var expectedMessage = UnavailableResultValueException.MESSAGE;
        Assert.Equal(expectedMessage, ex.Message);
    }


    [Fact]
    public async Task RunAsync_WhenResultIsFailureButThrowsAnExceptionInsteadOfAResult_ShouldPropagateThisExceptionUpToTheCaller()
    {
        //--- Arrange ---
        someServiceMock.Setup(srv => srv.FindData(It.IsAny<long>())).Throws(new ISomeService.AnotherException());
        string expectedExceptionMessage = ISomeService.AnotherException.MESSAGE;

        MyQueryUseCase useCase = new(someServiceMock.Object);
        MyUseCaseRequest request = new(SomePositiveId: 1);

        CancellationTokenSource cts = new();
        CancellationToken token = cts.Token;


        //--- Act && Assert ---
        Task<ISomeService.AnotherException> tskEx =
            Assert.ThrowsAsync<ISomeService.AnotherException>(() => useCase.RunAsync(request, token));

        ISomeService.AnotherException ex = await tskEx;

        Assert.Equal(expectedExceptionMessage, ex.Message);
    }


    #region CancellationToken
    [Fact]
    public async Task RunAsync_WhenResultIsFailureDueToCancellationTokenActivatitedBeforeCall_ShouldReturnATaskOfFailedResultWithTheCorrectError()
    {
        //--- Arrange ---
        FindDataDtoResponse serviceResponse = new(10, "SomeString");
        someServiceMock.Setup(srv => srv.FindData(It.IsAny<long>())).Returns(serviceResponse);

        MyQueryUseCase useCase = new(someServiceMock.Object);
        long anyId = 5;
        MyUseCaseRequest request = new(SomePositiveId: anyId);


        CancellationTokenSource cts = new();
        CancellationToken token = cts.Token;
        cts.Cancel(); //<<<<<<<<<<<<

        //--- Act ---
        Result<MyUseCaseResponse> result = await useCase.RunAsync(request, token);

        //--- Assert ---
        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Equal(AsyncErrors.UseCaseHandlingTaskOperationCancellation, result.Error);
    }

    [Fact]
    public async Task RunAsync_WhenResultIsFailureDueToTaskCanceledExceptionThrownFromTheUseCase_ShouldReturnATaskOfFailedResultWithTheCorrectError()
    {
        //--- Arrange ---
        someServiceMock.Setup(srv => srv.FindData(It.IsAny<long>())).Throws(new TaskCanceledException());

        MyQueryUseCase useCase = new(someServiceMock.Object);
        long anyId = 5;
        MyUseCaseRequest request = new(SomePositiveId: anyId);


        CancellationTokenSource cts = new();
        CancellationToken token = cts.Token;


        //--- Act ---
        Result<MyUseCaseResponse> result = await useCase.RunAsync(request, token);

        ////--- Assert ---
        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Equal(AsyncErrors.UseCaseHandlingTaskCancellation, result.Error);
    }
    #endregion CancellationToken
    #endregion RunAsync


    #region Run
    [Fact]
    public void Run_WhenResultIsSuccess_ShouldReturnATaskOfSuccessfulResultWithTheCorrectResponseValue()
    {
        //--- Arrange ---
        FindDataDtoResponse serviceResponse = new(10, "SomeString");
        someServiceMock.Setup(srv => srv.FindData(It.IsAny<long>())).Returns(serviceResponse);

        MyQueryUseCase useCase = new(someServiceMock.Object);
        long anyId = 5;
        MyUseCaseRequest request = new(SomePositiveId: anyId);
        MyUseCaseResponse expectedResponse = new(SomeIntValue: serviceResponse.IntValue, SomeStringValue: serviceResponse.StringValue);


        //--- Act ---
        Result<MyUseCaseResponse> result = useCase.Run(request);

        //--- Assert ---
        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Null(result.Error);
        Assert.Equal(expectedResponse, result.Value);
    }

    [Fact]
    public void Run_WhenResultIsFailureAndReturnsAResult_ShouldReturnATaskOfFailedResultWithTheCorrectError()
    {
        //--- Arrange ---
        long unexistingId = 10;
        someServiceMock.Setup(srv => srv.FindData(unexistingId)).Throws(new ISomeService.UnfoundIdException(unexistingId));
        string expectedExceptionMessage = string.Format(ISomeService.UnfoundIdException.MESSAGE_FORMAT, unexistingId);

        MyQueryUseCase useCase = new(someServiceMock.Object);
        MyUseCaseRequest request = new(SomePositiveId: unexistingId);


        //--- Act ---
        Result<MyUseCaseResponse> result = useCase.Run(request);

        //--- Assert ---
        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Equal(new Error(MyQueryUseCase.UNFOUND_ID_ERROR_CODE, expectedExceptionMessage), result.Error);


        var ex = Assert.Throws<UnavailableResultValueException>(() => result.Value);
        var expectedMessage = UnavailableResultValueException.MESSAGE;
        Assert.Equal(expectedMessage, ex.Message);
    }


    [Fact]
    public void Run_WhenResultIsFailureButThrowsAnExceptionInsteadOfAResult_ShouldPropagateThisExceptionUpToTheCaller()
    {
        //--- Arrange ---
        someServiceMock.Setup(srv => srv.FindData(It.IsAny<long>())).Throws(new ISomeService.AnotherException());
        string expectedExceptionMessage = ISomeService.AnotherException.MESSAGE;

        MyQueryUseCase useCase = new(someServiceMock.Object);
        MyUseCaseRequest request = new(SomePositiveId: 1);


        //--- Act && Assert ---
        ISomeService.AnotherException ex = Assert.Throws<ISomeService.AnotherException>(() => useCase.Run(request));
        Assert.Equal(expectedExceptionMessage, ex.Message);
    }
    #endregion Run

}

class MyQueryUseCase : QueryUseCase<MyUseCaseRequest, MyUseCaseResponse>
{
    public const string UNFOUND_ID_ERROR_CODE = "Unfound.Id";

    private ISomeService service;
    public MyQueryUseCase(ISomeService service)
    {
        this.service = service;
    }

    protected override Result<MyUseCaseResponse> Handle(MyUseCaseRequest request)
    {
        FindDataDtoResponse serviceResponse;
        try
        {
            serviceResponse = service.FindData(request.SomePositiveId);
        }
        catch (ISomeService.UnfoundIdException ex)
        {
            Error error = new(UNFOUND_ID_ERROR_CODE, ex.Message);
            return Result<MyUseCaseResponse>.NotOk(error);
        }

        MyUseCaseResponse response = new(SomeIntValue: serviceResponse.IntValue, SomeStringValue: serviceResponse.StringValue);
        return Result<MyUseCaseResponse>.Ok(response);
    }
}

