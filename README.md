# Minixer

[![Build Status](https://dev.azure.com/jsportaro/Minixer/_apis/build/status/jsportaro.Minixer?branchName=master)](https://dev.azure.com/jsportaro/Minixer/_build/latest?definitionId=2&branchName=master)

CI/CD for Minux Version 2

## What does it do?

Minux kernel code pushed to a given Github repository will

1. Trigger the creation of a new Virtual Machine (QEMU)
1. Have that VM mount the new branch in its file system
1. Build and install the branch
1. Provide an interface in browser to test the new build

The last step is a bit out of scope for a CI/CD server... But, what the heck

## Why are you making this?

Because the tools to edit code on Minux are limited and I like IDEs.  And, I just felt like making something
