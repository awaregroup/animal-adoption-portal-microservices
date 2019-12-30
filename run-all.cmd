@echo off
dotnet dev-certs https --trust
if %errorlevel% neq 0 pause && exit /b %errorlevel%

cd "%~dp0\Portal\AnimalAdoption.Web.Portal\ClientApp"
call npm install 
if %errorlevel% neq 0 pause && exit /b %errorlevel%
cd "%~dp0"

set ASPNETCORE_ENVIRONMENT=Development

start /D "%~dp0\Portal\AnimalAdoption.Web.Portal\" dotnet watch run -c Debug
start /D "%~dp0\Information\AnimalAdoption.Service.AnimalInformation.Api\" dotnet watch run -c Debug
start /D "%~dp0\Cart\AnimalAdoption.Service.Cart.Api\" dotnet watch run -c Debug
start /D "%~dp0\Identity\AnimalAdoption.Web.Identity\" dotnet watch run -c Debug
start /D "%~dp0\Image\AnimalAdoption.Service.Image.Api\" dotnet watch run -c Debug