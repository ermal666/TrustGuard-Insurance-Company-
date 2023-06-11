//import logo from './logo.svg';
import './App.css';
import { NavBar } from './components/NavBar';
import { Banner } from './components/Banner';
import { Insurances } from './components/Insurances';
import { Blogs } from './components/Blogs';
import { Contact } from './components/Contact';
import { Newsletter } from './components/Newsletter';
import { Footer } from './components/Footer';
import "bootstrap/dist/css/bootstrap.min.css";
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import IndividualInsurance from './components/IndividualInsurance';



function App() {
  return (
    <Router>
    <div className="App">
     <NavBar />
     <Banner />
     <Insurances/>
     <Routes>
     <Route path='/insurances' Component={Insurances}/>
      <Route path='/individualinsurance' Component={IndividualInsurance}/>
     </Routes>
     <Blogs />
     <Contact/>
    <Footer/>
     
    </div>
    </Router>
  );
}

export default App;
