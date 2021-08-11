#!/bin/bash

gcsfuse --key-file=/app/key/book-255910-1e410cf3767f.json chimenesjr-ingest-files /app/saved-files

dotnet file-ingest-back.dll
