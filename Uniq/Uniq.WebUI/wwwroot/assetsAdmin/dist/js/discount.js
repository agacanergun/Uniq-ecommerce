
// HTML'deki input alanlarını seçme
var priceInput = document.getElementById("price");
var discountRateInput = document.getElementById("discountrate");
var discountedPriceInput = document.getElementById("discountedprice");

// Input alanlarının değerleri değiştiğinde hesaplama yapma fonksiyonu
function calculateDiscountedPrice() {
    // Fiyat ve indirim oranı değerlerini alın
    var price = parseFloat(priceInput.value);
    var discountRate = parseFloat(discountRateInput.value);

    // İndirimli fiyatı hesapla
    var discountedPrice = price - (price * (discountRate / 100));

    // İndirimli fiyatı discountedPrice input alanına yazdır
    discountedPriceInput.value = discountedPrice.toFixed(2); // İstediğiniz ondalık hane sayısına göre düzenleyebilirsiniz
}

// Price ve discountRate input alanlarındaki değerler değiştiğinde hesaplama fonksiyonunu çağır
priceInput.addEventListener("input", calculateDiscountedPrice);
discountRateInput.addEventListener("input", calculateDiscountedPrice);
