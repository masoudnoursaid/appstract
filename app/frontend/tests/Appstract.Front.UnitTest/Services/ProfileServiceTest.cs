using System.Threading.Tasks;
using Appstract.Front.Application.Services;
using Appstract.Front.Infrastructure.Services;
using AutoFixture;
using FluentAssertions;
using NSubstitute;
using Appstract.Front.Domain.Models.ApiResponseModels;
using Appstract.Front.Domain.Models.Profile;
using UltraTone.ClientSdk.Customer.Web.V1;
using Error = Appstract.Front.Domain.Models.ApiResponseModels.Error;

namespace UltraTone.UnitTest.Services;

public class ProfileServiceTest
{
    private readonly IProfileClient _profileClientSubstitute;
    private readonly IErrorMessageService _errorMessageServiceSubstitute;
    private readonly Fixture _fixture = new();

    public ProfileServiceTest()
    {
        _profileClientSubstitute = Substitute.For<IProfileClient>();
        _errorMessageServiceSubstitute = Substitute.For<IErrorMessageService>();
    }

    [Fact]
    public async Task GetProfileAsync_ResponseIsSuccess()
    {
        GetProfileResultDto profileResultDto = _fixture.Build<GetProfileResultDto>().Create();
        GetProfileResponse profileResponse = new()
        {
            Success = true,
            Data =profileResultDto
        };
        _profileClientSubstitute.ProfileAsync()
            .Returns(profileResponse);
        ProfileService profileService = new (_profileClientSubstitute, _errorMessageServiceSubstitute);
        
        ApiResponseBase<ProfileInfoResponse> result = await profileService.GetProfileSettingAsync();
        
        Assert.True(result.Success);
        result.Data.Should().NotBeNull();
        result.Data.Should().NotBeEquivalentTo(new ProfileInfoResponse());
        result.Error.Should().BeEquivalentTo(new Error());
    }
    
    [Fact]
    public async Task GetProfileAsync_ResponseIsFailed_ReturnAnError()
    {
        GetProfileError topupVoucherError = _fixture.Build<GetProfileError>().Create();
        GetProfileResponse profileResponse = new()
        {
            Success = false,
            Error = topupVoucherError
        };
        _profileClientSubstitute.ProfileAsync().Returns(profileResponse);
        ProfileService profileService = new (_profileClientSubstitute, _errorMessageServiceSubstitute);
        
        ApiResponseBase<ProfileInfoResponse> result = await profileService.GetProfileSettingAsync();
        
        Assert.False(result.Success);
        result.Error.Should().NotBeNull();
        result.Error.Message.Should().NotBeNull();
        result.Error.Should().NotBeEquivalentTo(new Error());
        result.Data.Should().BeEquivalentTo(new ProfileInfoResponse());
    }
    
    [Fact]
    public async Task GetProfileAsync_ResponseIsNull_ThrowsException()
    {
        _profileClientSubstitute.ProfileAsync().Returns((GetProfileResponse) null!);
        ProfileService profileService = new (_profileClientSubstitute, _errorMessageServiceSubstitute);
        
        ApiResponseBase<ProfileInfoResponse> result = await profileService.GetProfileSettingAsync();
        
        Assert.False(result.Success);
        result.Error.Message.Should().BeEmpty();
        result.Error.Should().BeEquivalentTo(new Error());
        result.Data.Should().BeEquivalentTo(new ProfileInfoResponse());
    }
}