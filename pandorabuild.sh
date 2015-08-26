#!/bin/bash

git pull

# codeblocks --target="Release Pandora" --build JANE.v4.cbp
codeblocks --target="Debug Pandora" --build JANE.v4.cbp
