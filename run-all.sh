#!/bin/sh
SCRIPTPATH=$(dirname $0)

set -xe
dotnet dev-certs https --trust

(cd "${SCRIPTPATH}/Portal/AnimalAdoption.Web.Portal/ClientApp" && npm install && cd ../../..)

dotnet build "${SCRIPTPATH}/Portal/AnimalAdoption.Web.Portal/"
dotnet build "${SCRIPTPATH}/Information/AnimalAdoption.Service.AnimalInformation.Api/"
dotnet build "${SCRIPTPATH}/Cart/AnimalAdoption.Service.Cart.Api/"
dotnet build "${SCRIPTPATH}/Identity/AnimalAdoption.Web.Identity/"
dotnet build "${SCRIPTPATH}/Image/AnimalAdoption.Service.Image.Api/"

trap 'killall dotnet' SIGTERM

(dotnet watch --project "${SCRIPTPATH}/Portal/AnimalAdoption.Web.Portal/" run &
dotnet watch --project "${SCRIPTPATH}/Information/AnimalAdoption.Service.AnimalInformation.Api/" run &
dotnet watch --project "${SCRIPTPATH}/Cart/AnimalAdoption.Service.Cart.Api/" run &
dotnet watch --project "${SCRIPTPATH}/Identity/AnimalAdoption.Web.Identity/" run &
dotnet watch --project "${SCRIPTPATH}/Image/AnimalAdoption.Service.Image.Api/" run)
