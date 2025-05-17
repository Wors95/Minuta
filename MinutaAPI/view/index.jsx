import React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faSearch, faShoppingCart, faUser } from '@fortawesome/free-solid-svg-icons';
import './Navbar.css';

const Navbar = () => {
  return (
    <nav className="navbar">
      <div className="navbar-logo">
        <a href="/">Casio Store</a>
      </div>
      <ul className="navbar-links">
        <li><a href="/home">Home</a></li>
        <li><a href="/produtos">Produtos</a></li>
        <li><a href="/sobre">Sobre</a></li>
        <li><a href="/contato">Contato</a></li>
      </ul>
      <div className="navbar-icons">
        <FontAwesomeIcon icon={faSearch} className="icon" />
        <FontAwesomeIcon icon={faShoppingCart} className="icon" />
        <FontAwesomeIcon icon={faUser} className="icon" />
      </div>
    </nav>
  );
};

export default Navbar;
