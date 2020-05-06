#!/bin/sh
SCRIPTPATH=$(dirname $0)

set -xe
dotnet dev-certs https --trust

(cd "${SCRIPTPATH}/../Portal/Portal.Web/ClientApp" && npm install && cd ../../..)

dotnet build "${SCRIPTPATH}/../Portal/Portal.Web/"
dotnet build "${SCRIPTPATH}/../Information/AnimalInformation.Api/"
dotnet build "${SCRIPTPATH}/../Cart/Cart.Api/"
dotnet build "${SCRIPTPATH}/../Identity/Identity.Web/"
dotnet build "${SCRIPTPATH}/../Image/Image.Api/"

trap 'killall dotnet' SIGTERM

(dotnet watch --project "${SCRIPTPATH}/../Portal/Portal.Web/" run &
dotnet watch --project "${SCRIPTPATH}/../Information/AnimalInformation.Api/" run &
dotnet watch --project "${SCRIPTPATH}/../Cart/Cart.Api/" run &
dotnet watch --project "${SCRIPTPATH}/../Identity/Identity.Web/" run &
dotnet watch --project "${SCRIPTPATH}/../Image/Image.Api/" run)
