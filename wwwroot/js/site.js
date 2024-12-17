const slider = document.querySelector('.slider');
const sliderItems = document.querySelectorAll('.slider-item');
const prevButton = document.querySelector('.prev');
const nextButton = document.querySelector('.next');

let currentIndex = 0;
let isAnimating = false;

function moveSlide(direction) {
    if (isAnimating) return;

    isAnimating = true;

    currentIndex += direction;

    if (currentIndex < 0) {
        currentIndex = sliderItems.length - 1;
    } else if (currentIndex >= sliderItems.length) {
        currentIndex = 0;
    }

    const translateValue = -currentIndex * 100;
    slider.style.transform = `translateX(${translateValue}%)`;

    setTimeout(() => {
        isAnimating = false;
    }, 500); 
}


nextButton.addEventListener('click', () => moveSlide(1));
prevButton.addEventListener('click', () => moveSlide(-1));
