import './App.css';
import {Kart} from './Kart';
import {Warehouse} from './Warehouse';
import {Equipment} from './Equipment';
import {BrowserRouter, Route, Routes ,NavLink} from 'react-router-dom';

function App() {
  return (
    <BrowserRouter>
    <div className="App container">
      <h3 className="d-flex justify-content-center m-3">
        Учет складов и оборудования
      </h3>
        
      <nav className="navbar navbar-expand-sm bg-light navbar-dark">
        <ul className="navbar-nav">
          <li className="nav-item- m-1">
            <NavLink className="btn btn-light btn-outline-primary" to="/kart">
              Карты
            </NavLink>
          </li>
          <li className="nav-item- m-1">
            <NavLink className="btn btn-light btn-outline-primary" to="/warehouse">
              Склады
            </NavLink>
          </li>
          <li className="nav-item- m-1">
            <NavLink className="btn btn-light btn-outline-primary" to="/equipment">
              Экипировка
            </NavLink>
          </li>
        </ul>
      </nav>

      <Routes>
        <Route path='/kart' element={  <Kart />}/>
        <Route path='/warehouse' element={  <Warehouse />}/>
        <Route path='/equipment' element={  <Equipment />}/>
      </Routes>
    </div>
    </BrowserRouter>
  );
}

export default App;
