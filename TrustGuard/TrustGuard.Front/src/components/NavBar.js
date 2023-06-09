import { Navbar ,Container, Nav } from "react-bootstrap";
import { useState, useEffect } from "react";
import logo from "../assets/img/logo.png";
import estate from "../assets/img/estate.svg";
import facebook from "../assets/img/facebook.png";
import instagram from "../assets/img/instagram.png";
import whatsapp from "../assets/img/whatsapp.png";
import { Login } from "./Login";
import { Register } from "./Register";

export const NavBar = () => {
    const [activeLink, setActiveLink] = useState('home');
    const [scrolled, seScrolled] = useState (false);
    const [handleLoginClick, setShowLogin] = useState (false);
    
      
    

    useEffect( () =>{
     const onScroll = () => {
        if (window.scrollY > 50){
            seScrolled (true);
        }else{
          seScrolled(false);  
        }
     }
     window.addEventListener("scroll", onScroll);
     return () => window.removeEventListener("scroll", onScroll);
    }, [])
    const onUpdateActiveLink = (value) =>{
        setActiveLink(value);
    }
    
  return (
    <Navbar  expand="lg" className={scrolled ? "scrolled": ""}>
      <Container>
        <Navbar.Brand href="#home">
            <img src={logo} alt="Logo" />
        </Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav">
          <span className="navbar-toggler-icon"></span>  
             </Navbar.Toggle>
        <Navbar.Collapse id="basic-navbar-nav">
          <Nav className="me-auto">
            <Nav.Link href="#home" className={activeLink === 'home' ? 'active navbar-link' :'navbar-link'} onClick={() => onUpdateActiveLink('home')}>Faqja kqyesore</Nav.Link>
            <Nav.Link href="#blog" className={activeLink === 'blog' ? 'active navbar-link' :'navbar-link'} onClick={() => onUpdateActiveLink('blog')}>Blog</Nav.Link>
            <Nav.Link href="#insurances" className={activeLink === 'insurances' ? 'active navbar-link' :'navbar-link'}onClick={() => onUpdateActiveLink('insurances')}>Sigurimet</Nav.Link>
            <Nav.Link href="#newsletter" className={activeLink === 'newsletter' ? 'active navbar-link' :'navbar-link'}onClick={() => onUpdateActiveLink('newsletter')}>Rreth Nesh</Nav.Link>
          </Nav>
          <span className="navbar-text" >
            <div className="social-icon">
              <a href="#"><img src={facebook} alt="" /></a>
              <a href="#"><img src={instagram} alt="" /></a>
              <a href="#"><img src={whatsapp} alt="" /></a>  
            </div>
            <button onClick={handleLoginClick}>Login</button>
           
           
            {/*<button className="vvd" onClick={() => console.log('connect')}><span> <Login/></span></button>*/}
          </span>
        
        </Navbar.Collapse>
      </Container> 
    </Navbar>
  )
}

