# ThirdPersonShooterGame

#PROJE HAKKINDA
- Bu proje Yazılım Geliştirme Laboratuvarı kapsamında ödev olarak yapılmıştır.
- Third Person Shooter şeklindeki oyunların temeli ve mantığını öğrenmek hedeflenmiştir.
  
#OYUN SENARYOSU
- Bir asker olduğumuz bu oyunda amaç rehineyi kurtarmaktır. Rehine canavarlarla dolu bir alanda tutsak haldedir. Bu kısım oyunun sadece bir levelidir.

#SİSTEM AKIŞI AÇIKLAMASI
  1. ANA MENÜ
      - Oyuncu “Play” tuşuna bastığında oyun sahnesi yüklenir (SceneManager.LoadScene("GameScene")).
      - “Exit” tuşuna basarsa Application.Quit() ile oyun kapanır.
  2. OYUN SAHNELERİ
      - Oyuncu karakteri serbestçe hareket eder (WASD + Mouse).
      - Oyuncunun elinde bir silah vardır, ateş etme mekanikleri çalışır.
      - NPC’ler oyuncuya saldırır.
      - Oyuncu rehinenin kafesine geldiğinde sistem OnTriggerEnter() ile algılar.
      - Oyuncu kafese yaklaştığında ekranda “Press E to Rescue” yazısı çıkar.
      - Oyuncu E tuşuna bastığında kazanma olayı tetiklenir:
  3. KAZANMA EKRANI
      - “Tekrar Oyna” butonuna basılırsa oyun sıfırlanır.
      - “Exit” butonuna basılırsa oyun kapanır.


# MEAKANİKLERİN BLOK DİYAGRAMI
    ANA MENÜ       
  [Play]   [Exit]    

        │
        │ Play'e basılırsa
        ▼

      OYUN SAHNESİ      
                        
  - Oyuncu hareket eder  
  - NPC'lerle savaşır    
  - Silah ile ateş eder  
  - Can sistemi çalışır  
  - Düşmanlar ölür       

        │
        │ Oyuncu kafese ulaşır
        ▼

    KAFESE YAKLAŞILIR    
  "E" tuşuna basılır     
  → Kurtarma olayı olur  
        │
        │
        ▼

    KAZANMA EKRANI       
  "Tebrikler, Kazandın!" 
  [Tekrar Oyna] [Exit]   

        │
        ├──► Tekrar Oyna → Oyun sahnesini yeniden başlat
        │
        └──► Exit → Oyunu kapat


#OYUN MEKANİKLERİ
  1. NPC HAREKETLERİ
      - Idle, Patrol, Chase, Attack mekaniklerine sahiptir.
      - NavMeshAgent kullanarak belirlenen waypointler arasında devriye gezmektedirler.
      - Player' ı görünce Chase State' e geçip en kısa yoldan kovalamaya başlarlar.
  2. OYUNCU HAREKETLERİ
      - Movement State script' i ile karakterimiz hareket etmektedir.
      - Karakterimiz yürüyebilir, nişan alabilir, ateş edebilir, eğilebilir, cover alabilir.
  3. TRIGGER ALANLARI
      - NPC görüşüne girdiğimizde Chase State triggerlanır.
      - Tutsakın yanına gittiğimizde kurtarma scripti triggerlanır ve oyun sonu ekranı gelir.
  
  #TASARLANAN SAHNELER
  1. ANA MENÜ SAHNESİ
      - Oyunu başlatma ve çıkış tuşları bulunmakta
  2. OYUN SAHNESİ
      - Oyunu oynadığımız sahnedir.
      - Bir bilim kurgu deposunda geçmektedir.
      - İçerisinde NPC ler bulunmakta.
  3. KAZANMA SAHNESİ
      - Tebrik mesajı içerir.
      - Tekrar oyna ve çıkış butonları bulunur.

#LİTERATÜR TARAMASI VE ÖRNEK ÇALIŞMALAR
  - Oyunu tasarlarken youtube üzerinden birçok TPS oyun tutorialları taranmıştır.
  - Mekaniklerin kodlanmasında bu kaynaklardan yardım alınmış fakat birebir kodlar kullanılmamıştır.

#KULLANILAN YAZILIM MİMARİLERİ
- Proje Unity, Visual Studio, Visual Studio Code kullanılarak yapılmıştır.
- C# dili ve Unity kütüphaneleri proje genelinde kullanılmıştır.

#KARŞILAŞILAN ZORLUKLAR
- NPC' ler önce kapsül olarak tasarlanmış ve kodlanmış daha sonra karakterler eklenmiştir. Bu sebeple karakter ve animasyon eklemede zorlanılmıştır.
- Nişan alma ve ateş etme mekaniklerinde zorlanılmıştır.
- Youtube, Reddit gibi sitelerde araştırmalar yapılarak çözüm yolları bulunmuştur.

#PROJEDEN KAZANIMLAR
- Unity arayüzü ve kütüphaneleri öğrenilmiş, unity deneyimi kazanılmıştır.
- NPC mekanikleri State Machine nasıl çalışır genel yapısı nedir öğrenilmiştir.
- Animator kullanımı öğrenilmiştir.
- Git ve GitHub kullanmada deneyim kazanılmıştır.
