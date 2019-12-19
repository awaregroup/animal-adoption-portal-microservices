#!/bin/sh
SCRIPTPATH=$(dirname $(readlink -f "$0"))

set -xe
dotnet dev-certs https

(cd "${SCRIPTPATH}/AnimalAdoption.Web.Portal/AnimalAdoption.Web.Portal/ClientApp" && npm install && cd ${SCRIPTPATH})

dotnet build "${SCRIPTPATH}/AnimalAdoption.Web.Portal/AnimalAdoption.Web.Portal/"
dotnet build "${SCRIPTPATH}/AnimalAdoption.Service.AnimalInformation/AnimalAdoption.Service.AnimalInformation.Api/"
dotnet build "${SCRIPTPATH}/AnimalAdoption.Service.Cart/AnimalAdoption.Service.Cart.Api/"
dotnet build "${SCRIPTPATH}/AnimalAdoption.Web.Identity/AnimalAdoption.Web.Identity/"
dotnet build "${SCRIPTPATH}/AnimalAdoption.Service.Image/AnimalAdoption.Service.Image.Api/"

start dotnet run --project "${SCRIPTPATH}/AnimalAdoption.Web.Portal/AnimalAdoption.Web.Portal/"
start dotnet run --project "${SCRIPTPATH}/AnimalAdoption.Service.AnimalInformation/AnimalAdoption.Service.AnimalInformation.Api/"
start dotnet run --project "${SCRIPTPATH}/AnimalAdoption.Service.Cart/AnimalAdoption.Service.Cart.Api/"
start dotnet run --project "${SCRIPTPATH}/AnimalAdoption.Web.Identity/AnimalAdoption.Web.Identity/"
start dotnet run --project "${SCRIPTPATH}/AnimalAdoption.Service.Image/AnimalAdoption.Service.Image.Api/"
read -p "Press enter to continue..."
