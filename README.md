# 🖼️ Image_Compression

Rasmlarni avtomatik tarzda turli o‘lchamlarga (Large, Medium, Small) siqish (compress) uchun yozilgan ASP.NET Core backend kutubxonasi. Ushbu loyiha orqali yuklangan har bir rasm quyidagi o'lchamlarga avtomatik saqlanadi:

| Type    | Original | Large   | Medium  | Small  |
|---------|----------|---------|---------|--------|
| Size    | 2 MB     | ~200 KB | ~70 KB  | ~10 KB |

---

## ✨ Texnologiyalar

- `.NET 8`, `ASP.NET Core`
- `ImageSharp`
- `Magick.NET`
- `Bitmap`

---

## 🔧 Foydalanish

1. Rasmni yuklang (`IFormFile`)
2. Har bir yuklangan rasm `images/Original`, `images/Large`, `images/Medium`, `images/Small` papkalarga `.webp` formatda saqlanadi.
3. Har o‘lcham `ImageType` enum orqali belgilanadi (`Original`, `Large`, `Medium`, `Small`).

---

## ✅ Foydalari

- **💾 Saqlash joyini tejaydi**  
  Har bir rasmning o'lchami sezilarli darajada kamaytiriladi (masalan, 2 MB → 10 KB), bu esa serverda ko'proq rasm saqlash imkonini beradi.

- **⚡ Tez yuklanadi**  
  Saytingizda kichik hajmdagi rasm ishlatilganda sahifa yuklanish tezligi oshadi — foydalanuvchi tajribasi yaxshilanadi.

- **📱 Mobil trafikni tejaydi**  
  Mobil foydalanuvchilarga kichik hajmdagi rasm ko‘rsatilsa, ularning internet trafik sarfi kamayadi.

- **🌐 SEO va PageSpeed yaxshilanadi**  
  Google PageSpeed kabi vositalar saytni baholaganda rasm optimallashtirilgan bo'lishini talab qiladi — bu vosita aynan shunga xizmat qiladi.

- **🧩 Moslashuvchan arxitektura**  
  Turli kompressorlar (`ImageSharp`, `Magick.NET`, `Bitmap`) bilan ishlash imkonini beradi — sizga qulayini tanlab, ishlatishingiz mumkin.

- **🔄 Avtomatlashtirilgan jarayon**  
  Bir marta yuklangan rasm avtomatik tarzda barcha kerakli o‘lchamlarga siqiladi — qo‘lda tahrirlash, kesish, formatlash shart emas.

- **🧪 Testga tayyor**  
  Siqilgan rasm sifatini va hajmini avtomatik tekshirish uchun moslashuvchanlik mavjud.

---

## 🗂️ Papkalar tuzilmasi

