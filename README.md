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

# Space Exploration project Spring 2024

## Assets
This folder contains all the data files and the scenes. There are 3 sub folders:

# DataFiles
This folder contains the downloaded datafiles as well as the script used to convert the csv into usable formats.

# StreamingAssets
This folder contains the converted data file to be bundled in the app build.

# module-omicron
This is the main chunk of the app. It contains all the prefabs and the scripts. All the objects used in the application are present according to folder names.

## Build
This folder contains the builds done on my system to test the app. This is not important to run on another system. It needs to be rebuilt.