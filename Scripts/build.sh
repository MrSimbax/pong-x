#! /bin/sh

project="Pong-X"
 
echo "Attempting to build $project for Windows"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -nographics \
  -silent-crashes \
  -logFile $(pwd)/unity.log \
  -projectPath "$(pwd)/$project" \
  -buildWindowsPlayer "$(pwd)/Build/$project-win/$project.exe" \
  -quit
 
echo "Attempting to build $project for OS X"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -nographics \
  -silent-crashes \
  -logFile $(pwd)/unity.log \
  -projectPath "$(pwd)/$project" \
  -buildOSXUniversalPlayer "$(pwd)/Build/$project-osx/$project.app" \
  -quit
 
echo "Attempting to build $project for Linux"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -nographics \
  -silent-crashes \
  -logFile $(pwd)/unity.log \
  -projectPath "$(pwd)/$project" \
  -buildLinuxUniversalPlayer "$(pwd)/Build/$project-lin/$project" \
  -quit
 
echo 'Logs from build'
cat $(pwd)/unity.log

echo 'Packing the build files to zip files'

echo 'Generating README and VERSION files...'
cp README.md Build/$project-win/README.txt
cp README.md Build/$project-lin/README.txt
cp README.md Build/$project-osx/README.txt
git describe --long > Build/$project-win/VERSION.txt
git describe --long > Build/$project-lin/VERSION.txt
git describe --long > Build/$project-osx/VERSION.txt

cd Build

echo 'Removing debug files...'
rm ./$project-win/*.pdb

echo 'Packing Windows build...'
zip -r $project-win.zip ./$project-win

echo 'Packing OSX build...'
zip -r $project-osx.zip ./$project-lin

echo 'Packing Linux build...'
zip -r $project-lin.zip ./$project-osx

cd ..
echo 'Zip files are ready to deploy.'