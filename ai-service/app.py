from fastapi import FastAPI
from pydantic import BaseModel
from transformers import pipeline
import os

app = FastAPI()

# Hugging Face cache dizini (Docker ENV'den alınacak)
sentiment_analyzer = pipeline("sentiment-analysis")

class Message(BaseModel):
    text: str

@app.post("/api/predict")
def predict_sentiment(message: Message):
    result = sentiment_analyzer(message.text)[0]
    sentiment = result["label"].lower()
    score = round(result["score"], 3)
    return {"sentiment": sentiment, "score": score}
