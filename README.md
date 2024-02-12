# omicron-unity CAVE2 Simulator
A example project for developing Unity3D applications for virtual reality environments such as the Electronic Visualization Laboratory's CAVE2(TM). This package and its associated submodule (https://github.com/arthurnishimoto/module-omicron) work as a wrapper around the CAVE2 API so that the CAVE2 environment can be simulated in Unity3D using stock Unity assets (no editor platform dependencies, professional licenses needed).

**Documentation:**

[Guide for Running Unity in CAVE2](https://github.com/uic-evl/omicron-unity/wiki/Guide-for-running-Unity-in-CAVE2)

[CAVE2 Unity Tips and Examples](https://github.com/uic-evl/omicron-unity/wiki/CAVE2-Unity-Tips-and-Examples)

[Guide for Running Unity in Continuum](https://github.com/uic-evl/omicron-unity/wiki/Guide-for-running-Unity-in-Continuum)

## Installation

This repository is a wrapper for the main Omicron-Unity module available at https://github.com/arthurnishimoto/module-omicron. Download from here for use as a Unity example project. Use the previous link for use as a submodule of an existing project.

If you are cloning using Git, remember to remember to use the recursive flag:

`git clone --recursive https://github.com/uic-evl/omicron-unity.git`

Alternatively when available, you can use one of the zipped release files.

CAVE2 is a trademark of the University of Illinois Board of Trustees

# Space Explorer - Spring 2024 Project

## Assets
This folder contains all the necessary data files and the scenes for the application to build successfully. There are 4 sub folders:

### DataFiles
This folder contains the downloaded datafiles as well as the python script used to convert the csv into usable formats. Main operations performed include removing of non usable data as well as converting the velocity vectors of stars from km/s to pc/year.

### StreamingAssets
This folder contains the converted data file to be bundled in the app build. Without this folder, none of the data will be found and the application will run with an empty sky.

### module-omicron
This is the main chunk of the app. It contains all the prefabs and the scripts. All the objects used in the application development are present according to folder names. This folder is mainly for the development of the app, not for the final build.

### Resources
This Folder houses the materials and shaders that will be used for the visual purposes.

# Build
This folder contains the builds done on my system to test the app. This is not important to run on another system. It needs to be rebuilt.
