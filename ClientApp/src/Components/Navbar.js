import React, { useState } from "react";
import { Link } from "react-router-dom";
import "./Navbar.css";
import { FaBars } from "react-icons/fa";
import { ImCross } from "react-icons/im";

const Navbar = () => {
  const [Mobile, setMobile] = useState(false);

  return (
    <nav className="navbar">
     
        <h3 className="logo">Logo</h3>
        <ul className={Mobile ? "nav-links-mobile" : "nav-links"} onClick={()=>setMobile(false)}> 
          <Link to="/">
            <li> Home </li>
          </Link>
          <Link to="/producto">
            <li> Producto </li>
          </Link>
          <Link to="/about">
            <li> About</li>
          </Link>
          <Link to="/services">
            <li> Services</li>
          </Link>
          <Link to="/skills">
            <li> Skills</li>
          </Link>
          <Link to="/contact">
            <li> Contact</li>
          </Link>
        </ul>
        <button className="mobile-menu-icon" onClick={()=>setMobile(!Mobile)}>
          {Mobile ? <ImCross /> : <FaBars />}
        </button>
      
    </nav>
  );
};
export default Navbar;
