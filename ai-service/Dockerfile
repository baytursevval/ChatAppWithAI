FROM python:3.10-slim

WORKDIR /app

# Gereken paketleri yükle
COPY requirements.txt .
RUN pip install --no-cache-dir -r requirements.txt

# Hugging Face cache klasörünü oluştur ve izin ver
RUN mkdir -p /app/huggingface && chmod -R 777 /app/huggingface
ENV HF_HOME=/app/huggingface

# Kodları kopyala
COPY . .

# FastAPI servisini başlat
CMD ["uvicorn", "app:app", "--host", "0.0.0.0", "--port", "7860"]
