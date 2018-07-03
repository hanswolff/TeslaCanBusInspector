#!/bin/sh

dotnet build TeslaCanBusInspector.sln && dotnet TeslaCanBusInspector.Site/bin/Debug/netcoreapp2.1/TeslaCanBusInspector.Site.dll
