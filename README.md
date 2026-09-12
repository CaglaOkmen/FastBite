# 🍔 FastBite

<div align="center">

![Unity](https://img.shields.io/badge/Unity-2D%20URP-black?logo=unity&style=for-the-badge)
![Language](https://img.shields.io/badge/Language-C%23-239120?logo=c-sharp&style=for-the-badge)
![Platform](https://img.shields.io/badge/Platform-WebGL%20%7C%20Windows-blue?style=for-the-badge)
![UI](https://img.shields.io/badge/UI-UI%20Toolkit-red?style=for-the-badge)
![Hosting](https://img.shields.io/badge/Hosting-Firebase-orange?logo=firebase&style=for-the-badge)

**Unity, C# ve UI Toolkit ile geliştirilmiş, 2D arcade tarzı burger hazırlama oyunu.**

Ekranda süzülen malzemeleri yakalayın, tarif kartına göre doğru sırayla dizin ve 30 dinamik seviye boyunca zamana karşı yarışın!

---

### 🎮 [👉 HEMEN OYNA / PLAY LIVE (Firebase WebGL)](https://fastbite-game.web.app) 👈

*İndirme gerektirmez — doğrudan internet tarayıcınızdan oynayabilirsiniz.*

---

</div>

## 📖 Oyun Hakkında

**FastBite**, oyuncuların rastgele üretilen tarif kartlarındaki malzemeleri doğru sırayla bir araya getirdiği tempolu bir 2D arcade oyunudur. Mutfak alanında süzülen malzemeler ekran sınırlarından seker; oyuncunun görevi doğru malzemeleri yakalayıp tabağa sürüklemek ve süre bitmeden hedef burger sayısına ulaşmaktır.

Oyun **30 seviye** boyunca ilerledikçe zorluk dinamik olarak artar: Yeni malzemeler açılır, burger tarifleri uzar, tamamlanması gereken hedef sayısı çoğalır ve seviye süresi daralır.

---

## ✨ Öne Çıkan Özellikler

- **📜 Dinamik ve Prosedürel Tarif Sistemi:**
  - Seviyeye göre ölçeklenen algoritmik burger tarifi üretimi.
  - Üst üste 3 kez aynı malzemenin gelmesini önleyen akıllı rastgelelik mantığı.
  - Seviye atladıkça genişleyen aktif malzeme havuzu.

- **🖐️ 2D Fizik & Drag-and-Drop Mekaniği:**
  - Viewport sınırlarından otomatik seken kinematik malzeme hareketi.
  - Yumuşak mouse Drag-and-Drop kontrolleri ve tabağa otomatik yerleşme (snap).
  - **LIFO (Last-In, First-Out) Yapısı:** Yanlış konan malzemeyi en üstten kolayca geri alabilme imkanı.

- **🏆 30 Seviyeli İlerleme & Kilit Sistemi:**
  - `currentLevel` değerine göre dinamik olarak hesaplanan hedef burger sayısı ve süre formülü.
  - `PlayerPrefs` ile tarayıcıda kalıcı olarak saklanan 30 seviyelik kilit/seçim menüsü.
  - Çift sayılı seviyelerde yeni bir malzemenin oyuna girdiğini gösteren açılış pop-up'ı.

- **🎨 Modern UI Toolkit (UIElements):**
  - Yüksek performanslı vektörel UXML ve USS tabanlı arayüz.
  - Seviye, Kalan Süre ve Hedef Sayacını (`Tamamlanan / Hedef`) anlık gösteren dinamik HUD.
  - Arka planı donduran (`Time.timeScale = 0`) ve fare etkileşimini kilitleyen Pause (Duraklatma) menüsü.
  - Doğru tarifte ışık efekti, hatalı tarifte kırmızı yanıp sönme görsel uyarısı.

- **🔊 Kapsamlı Ses Sistemi (AudioManager):**
  - Tüm ses efektlerini (Malzeme bırakma, Doğru tarif, Hatalı dizilim, Buton tıklama, Yeni malzeme) yöneten Singleton `AudioManager`.
  - Müzik ve Ses Efekti (SFX) için ayrı kanallara sahip `AudioMixer` entegrasyonu.
  - Sahne geçişlerinde ve menüye dönüşlerde ses seviyesini RAM'de koruyan statik slider kontrolleri.

- **🎮 WebGL & Responsive Web Desteği:**
  - WebGL formatında derlenip [Firebase Hosting](https://fastbite-game.web.app) üzerinde canlı olarak yayına alınmıştır.
  - UI Toolkit `PanelSettings` sayesinde farklı tarayıcı ve ekran çözünürlüklerine otomatik uyum sağlar.

---

### 🎯 Amaç:
1. Ekranın sağındaki tarif kartında görünen malzemeleri aşağıdan yukarıya doğru inceleyin.
2. Süzülen malzemeler arasından doğru olanları sırayla alt ekmeğin üzerine dizin.
3. Son doğru malzeme konulduğunda üst ekmek otomatik olarak kapanır ve burger tamamlanır.
4. Süre dolmadan önce seviye hedefini tamamlayarak bir sonraki seviyeye geçin!

---

## 📂 Proje Mimarisi

```plaintext
FastBite/
├── Assets/
│   ├── Scenes/
│   │   ├── 0 - MainMenu.unity      # Giriş ekranı, ses ayarları, sahne yönlendirmeleri
│   │   ├── 1 - LevelSelect.unity   # 30 seviyelik kilitli/açık seviye seçim ızgarası
│   │   └── 2 - GamePlay.unity      # Ana mutfak istasyonu ve oyun döngüsü
│   ├── Scripts/
│   │   ├── AudioManager.cs         # Ses efektlerini tetikleyen merkezi Singleton
│   │   ├── GameManager.cs          # Süre, hedef, seviye geçişleri ve oyun durumu
│   │   ├── ItemMotion.cs           # Malzemelerin ekranda süzülmesi ve sınır kontrolü
│   │   ├── LevelSelectManager.cs   # Seviye kilitleri ve sahne geçişleri
│   │   ├── MainMenuManager.cs      # Menü butonları, AudioMixer slider kontrolleri
│   │   ├── MouseInteraction.cs     # Drag-and-drop, tabak kontrolü ve tarif doğrulaması
│   │   └── RecipeCard.cs           # Algoritmik tarif üretimi ve malzeme havuzu
│   ├── UI Toolkit/                 # UXML şablonları, USS stilleri ve PanelSettings
│   ├── Audio/                      # SFX ses dosyaları ve MainAudioMixer
│   └── Sprites/                    # Malzemeler, butonlar ve arka planlar
```

---
