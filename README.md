# File-ingest-back

1. file-ingest-front

2. **file-ingest-back**

3. file-ingest-db

Receives the calls from file-ingest-front to create the files.

These Json files are created in a configured folder and automatically uploaded to Cloud Storage.

`dotnet run` -> http://localhost:5000


`dotnet test`

## GCP Env

http://localhost:8080

## KMS

`gcloud kms keyrings create file-ingest --location global`
`gcloud kms keys create file-ingest-back --location global --keyring file-ingest --purpose encryption`
`gcloud kms keys list --location global --keyring file-ingest`
`gcloud kms encrypt --location global --keyring file-ingest --key file-ingest-back --plaintext-file ../book-255910-1e410cf3767f.json --ciphertext-file key.encrypted`
`gcloud kms decrypt --location global --keyring file-ingest --key file-ingest-back --ciphertext-file key.encrypted --plaintext-file key.json`


### To Fix

Save files to GCS