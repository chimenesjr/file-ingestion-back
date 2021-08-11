provider "google" {
  credentials = "${file("./book-255910-cb84b5f5eb61.json")}"
  project     = "book-255910"
  region      = "us-central1"
  zone        = "us-central1-a"
}

resource "google_compute_instance" "file-ingest-back" {

  name         = "file-ingest-back"
  machine_type = "f1-micro"
  zone         = "us-central1-a"
  tags         = ["allow-sql", "http-server"]

  boot_disk {
    initialize_params {
      image = "ubuntu-os-cloud/ubuntu-1404-trusty-v20160602"
    }
  }

  network_interface {
    network = "default"

    access_config {
      # Ephemeral
    }
  }

  metadata_startup_script = <<SCRIPT
      sudo curl -sSL https://get.docker.com/ | sh
      sudo usermod -aG docker `echo $USER`
      sudo docker run -d -p 80:80 nginx
      sudo docker run -p 1433:8080 --name file-ingest-back gcr.io/book-255910/file-ingest-back@sha256:b8ef17effb326ec7f12fdb4456552d4fe435fda2ff7b038bd12e917521cbcd15
      SCRIPT

  service_account {
    scopes = ["https://www.googleapis.com/auth/compute.readonly"]
  }
}