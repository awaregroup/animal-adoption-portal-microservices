@echo off
dotnet dev-certs https --trust
if %errorlevel% neq 0 pause && exit /b %errorlevel%

cd "%~dp0\AnimalAdoption.Web.Portal\AnimalAdoption.Web.Portal\ClientApp"
call npm install 
if %errorlevel% neq 0 pause && exit /b %errorlevel%
cd "%~dp0"

dotnet build "%~dp0\AnimalAdoption.Web.Portal\AnimalAdoption.Web.Portal\"
if %errorlevel% neq 0 pause && exit /b %errorlevel%
dotnet build "%~dp0\AnimalAdoption.Service.AnimalInformation\AnimalAdoption.Service.AnimalInformation.Api\"
if %errorlevel% neq 0 pause && exit /b %errorlevel%
dotnet build "%~dp0\AnimalAdoption.Service.Cart\AnimalAdoption.Service.Cart.Api\"
if %errorlevel% neq 0 pause && exit /b %errorlevel%
dotnet build "%~dp0\AnimalAdoption.Web.Identity\AnimalAdoption.Web.Identity\"
if %errorlevel% neq 0 pause && exit /b %errorlevel%
dotnet build "%~dp0\AnimalAdoption.Service.Image\AnimalAdoption.Service.Image.Api\"
if %errorlevel% neq 0 pause && exit /b %errorlevel%

start /D "%~dp0\AnimalAdoption.Web.Portal\AnimalAdoption.Web.Portal\" dotnet run
start /D "%~dp0\AnimalAdoption.Service.AnimalInformation\AnimalAdoption.Service.AnimalInformation.Api\" dotnet run
start /D "%~dp0\AnimalAdoption.Service.Cart\AnimalAdoption.Service.Cart.Api\" dotnet run
start /D "%~dp0\AnimalAdoption.Web.Identity\AnimalAdoption.Web.Identity\" dotnet run
start /D "%~dp0\AnimalAdoption.Service.Image\AnimalAdoption.Service.Image.Api\" dotnet run
pause
