let slideIndext = 0;
        
function showSlides1() {
    const slides = document.querySelectorAll('.slidet');
    
    // Скрыть все слайды
    slides.forEach(slide => slide.classList.remove('activet'));
    
    // Перейти к следующему слайду
    slideIndext++;
    
    // Вернуться к первому слайду после последнего
    if (slideIndext > slides.length) {
        slideIndext = 1;
    }
    
    // Показать текущий слайд
    slides[slideIndext - 1].classList.add('activet');
    
    // Повторять каждые 10 секунд
    setTimeout(showSlides1, 5000);
    
}

// Запустить слайд-шоу при загрузке страницы
showSlides1();
