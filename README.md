# 💬 ChatEmotion – Duygu Analizli Sohbet Uygulaması

Bu proje, kullanıcıların rumuz (nickname) ile giriş yapıp birbirleriyle sohbet edebildiği,  
mesajların **AI tarafından canlı olarak duygu analizi** yapıldığı bir web projesidir.  

Backend, frontend ve AI servisi tamamen ücretsiz platformlarda deploy edilmiştir.

---

## 🚀 Özellikler (MVP)

- **Web (React)**: Basit ve anlaşılır chat ekranı  
- **Backend (.NET Core + SQLite)**: Kullanıcı kaydı, mesaj kaydı  
- **AI Servisi (Python + Hugging Face Spaces)**: Mesajların pozitif/negatif/neutral analizi  
- **Gerçek Zamanlı**: Mesaj gönderildiğinde backend AI servisine istek atar ve sonucu anlık gösterir  

---

## 🧩 Teknoloji ve Hosting

| Katman       | Teknoloji / Servis |
|-------------|------------------|
| Frontend    | React / React Native CLI |
| Backend     | .NET Core 7.0 + SQLite |
| AI Servisi  | Python + Hugging Face Spaces (Docker) |
| Deployment  | Vercel (Web), Render (Backend), Hugging Face Spaces (AI) |

---


** AI Servisi

Hugging Face Spaces Docker SDK kullanılarak deploy edilmiştir.
Local test için: http://localhost:7860/api/predict
Canlı test için: https://sevvaltzl-sentimentt.hf.space/api/predict


** Backend Servisi

```bash
git clone https://github.com/baytursevval/ChatEmotionAPI.git
cd ChatEmotionAPI
dotnet restore
dotnet ef database update
dotnet run

Render için Docker kullanılarak deploy edilmiştir.
Backend varsayılan olarak: https://localhost:5285
Render deploy sonrası canlı URL: https://chatemotionapi.onrender.com


** Frontend Servisi

git clone https://github.com/kullaniciadi/chat-web.git
cd chat-web
npm install
npm start
Localhost: http://localhost:3000
Canlı URL (Vercel): [https://chatemotion-frontend.vercel.app]https://chat-ev1zd4fwl-baytursevvals-projects.vercel.app/)

