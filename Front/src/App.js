import './App.css';
import Inicio from './componentes/Inicio';
import { Routes, Route } from 'react-router-dom'
import Login from './componentes/Login';
import Tienda from './componentes/Tienda';
import Carrito from './componentes/Carrito';
import Checkout from './componentes/Checkout';
import Success from './componentes/Success';

function App() {
  return (
    <Routes>
      <Route path='/' element={<Inicio />}></Route>
      <Route path='login' element={<Login />}></Route>
      <Route path='tienda' element={<Tienda />}></Route>
      <Route path='carrito' element={<Carrito />}></Route>
      <Route path='checkout' element={<Checkout />}></Route>
      <Route path='success' element={<Success />}></Route>
      <Route path="*" element={<Inicio />} />
    </Routes>
  );
}

export default App;