@echo off
dotnet dev-certs https --trust
if %errorlevel% neq 0 pause && exit /b %errorlevel%

cd "%~dp0\Portal\Portal.Web\ClientApp"
call npm install 
if %errorlevel% neq 0 pause && exit /b %errorlevel%
cd "%~dp0"

set ASPNETCORE_ENVIRONMENT=Development

start /D "%~dp0\Portal\Portal.Web\" dotnet watch run -c Debug
start /D "%~dp0\Information\AnimalInformation.Api\" dotnet watch run -c Debug
start /D "%~dp0\Cart\Cart.Api\" dotnet watch run -c Debug
start /D "%~dp0\Identity\Identity.Web\" dotnet watch run -c Debug
start /D "%~dp0\Image\Image.Api\" dotnet watch run -c Debug