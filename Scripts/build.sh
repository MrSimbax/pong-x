#! /bin/sh
 
# Example build script for Unity3D project. See the entire example: https://github.com/JonathanPorta/ci-build
 
# Change this the name of your project. This will be the name of the final executables as well.
project="Pong-X"
 
echo "Attempting to build $project for Windows"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -nographics \
  -silent-crashes \
  -logFile $(pwd)/unity.log \
  -projectPath "$(pwd)/Pong-X" \
  -buildWindowsPlayer "$(pwd)/Build/pong-x-win/$project.exe" \
  -quit
 
echo "Attempting to build $project for OS X"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -nographics \
  -silent-crashes \
  -logFile $(pwd)/unity.log \
  -projectPath "$(pwd)/Pong-X" \
  -buildOSXUniversalPlayer "$(pwd)/Build/pong-x-osx/$project.app" \
  -quit
 
echo "Attempting to build $project for Linux"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -nographics \
  -silent-crashes \
  -logFile $(pwd)/unity.log \
  -projectPath "$(pwd)/Pong-X" \
  -buildLinuxUniversalPlayer "$(pwd)/Build/pong-x-lin/$project" \
  -quit
 
echo 'Logs from build'
cat $(pwd)/unity.log

echo 'Packing the build files to zip files'
cd $(pwd)/Build/
echo 'Packing Windows build...'
zip -r pong-x-win.zip ./pong-x-win
rm -r *.pdb
echo 'Packing OSX build...'
zip -r pong-x-osx.zip ./pong-x-osx
echo 'Packing Linux build...'
zip -r pong-x-lin.zip ./pong-x-lin
echo 'Zip files are ready to deploy.'
cd ..