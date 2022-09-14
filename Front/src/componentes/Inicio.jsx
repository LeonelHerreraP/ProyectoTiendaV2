import '../App.css';
import '../css/InicioCss.css'
import Navbar from './Navbar';
import Footer from './Footer';
import Swal from 'sweetalert2'

function Inicio() {

  function Imagen() {
    Swal.fire({
      title: 'Julio Iglesias',
      text: 'De tanto ocultar la verdad con mentiras, Me engañé sin saber que era yo quien perdía.',
      imageUrl: 'https://unsplash.it/400/300',
      imageWidth: 400,
      imageHeight: 200,
      imageAlt: 'Custom image'
    });
  }

  return (
    <div className="App">
      <Navbar />
      <div id="carouselExampleInterval" className="carousel slide" data-bs-ride="carousel">
        <div className="carousel-indicators">
          <button type="button" data-bs-target="#carouselExampleInterval" data-bs-slide-to="0" className="active" aria-current="true" aria-label="Slide 1"></button>
          <button type="button" data-bs-target="#carouselExampleInterval" data-bs-slide-to="1" aria-label="Slide 2"></button>
          <button type="button" data-bs-target="#carouselExampleInterval" data-bs-slide-to="2" aria-label="Slide 3"></button>
        </div>
        <div className="carousel-inner">
          <div className="carousel-item active" data-bs-interval="10000">
            <img src='/img/electrodomesticos.jpg' className="d-block w-100 imgCaru" alt="Electrodomestico" />
          </div>
          <div className="carousel-item" data-bs-interval="2000">
            <img src='/img/PorLey.jpg' className="d-block  imgCaru w-100" alt="Electrodomestico" />
          </div>
          <div className="carousel-item">
            <img src='/img/Anuel.jpg' className="d-block imgCaru w-100" alt="Electrodomestico" />
          </div>
        </div>
      </div>

      <div className='contenedor'>

        <div className='divSugeridos'>
          <h2><span className="material-symbols-outlined azul">
            sell
          </span> Sugeridos</h2>
          <hr></hr>
        </div>

        <div className='rellenoFoto mb-2' onClick={Imagen}>
        </div>

        <div className='Relleno'>
          <div className='rellenoFoto2 mb-2' onClick={Imagen}>
          </div>
          <div className='rellenoFoto3 mb-2' onClick={Imagen}>
          </div>
        </div>

        <div className='divSugeridos'>
          <h2><span className="material-symbols-outlined azul">
            favorite
          </span> Categorías destacadas</h2>
          <hr></hr>
        </div>
        <div className="cateDestacadas">
          <div className="destacado"></div>
          <div className="destacado cafetera"></div>
          <div className="destacado lavadora"></div>
          <div className="destacado nevera"></div>
          <div className="destacado tv"></div>
          <div className="destacado estufa"></div>
        </div>

        <Footer />
      </div>
    </div>

  );
}

export default Inicio;
