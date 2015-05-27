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
  -buildWindowsPlayer "$(pwd)/Build/windows/$project.exe" \
  -quit
 
echo "Attempting to build $project for OS X"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -nographics \
  -silent-crashes \
  -logFile $(pwd)/unity.log \
  -projectPath "$(pwd)/Pong-X" \
  -buildOSXUniversalPlayer "$(pwd)/Build/osx/$project.app" \
  -quit
 
echo "Attempting to build $project for Linux"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -nographics \
  -silent-crashes \
  -logFile $(pwd)/unity.log \
  -projectPath "$(pwd)/Pong-X" \
  -buildLinuxUniversalPlayer "$(pwd)/Build/linux/$project" \
  -quit
 
echo 'Logs from build'
cat $(pwd)/unity.log