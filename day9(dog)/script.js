$(document).ready(function () {
  const API_KEY = "ruqiuJKIpiJhvWgk8kwYfnUCnhrBYeCFltPBFxp3VqXcZnL7Sd4IKRsc"; // Replace this with your real key

  $.ajax({
    method: "GET",
    url: "https://api.pexels.com/v1/search?query=dog&per_page=5",
    headers: {
      Authorization: API_KEY
    },
    success: function (data) {
      data.photos.forEach((photo, index) => {
        const isActive = index === 0 ? "active" : "";
        const imageUrl = photo.src.large; // or .original

        const slide = `
          <div class="carousel-item ${isActive}">
            <img src="${imageUrl}" class="d-block mx-auto" alt="Dog ${index + 1}">
          </div>
        `;
        $("#carouselContent").append(slide);
      });
    }
  });
});
