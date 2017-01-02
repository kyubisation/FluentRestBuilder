#!/usr/bin/env bash

#exit if any command fails
set -e

artifactsFolder="./artifacts"

if [ -d $artifactsFolder ]; then  
  rm -R $artifactsFolder
fi

dotnet restore

for dir in `find ./test/ -maxdepth 1 -mindepth 1 -iname "*.Test" -type d`
do
	dir=`basename $dir`
	# Ideally we would use the 'dotnet test' command to test netcoreapp and net451 so restrict for now 
	# but this currently doesn't work due to https://github.com/dotnet/cli/issues/3073 so restrict to netcoreapp

	dotnet test ./test/$dir -c Release -f netcoreapp1.0

	# Instead, run directly with mono for the full .net version 
	dotnet build ./test/$dir -c Release -f net451

	mono ./test/$dir/bin/Release/net451/*/dotnet-test-xunit.exe ./test/$dir/bin/Release/net451/*/$dir.dll
done


revision=${TRAVIS_JOB_ID:=1}  
revision=$(printf "ci-%04d" $revision)

for dir in `find ./src/ -maxdepth 1 -mindepth 1 -type d`
do
	dir=`basename $dir`
	dotnet pack ./src/$dir -c Release -o ./artifacts --version-suffix=$revision
done
