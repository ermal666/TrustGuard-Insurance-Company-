import { Alert, Col, Row } from "react-bootstrap";
import { useState, useEffect } from "react";
import Logo from "../assets/img/Trust GUARD (1).png";
import TrackVisibility from "react-on-screen";
import "animate.css";

export const Newsletter = ({onValidated, status, message}) => {
    const [email, setEmail] = useState ('');

    useEffect(() => {
     if (status === 'success') clearFields();
    }, [status])

    const handleSubmit = (e) => {
     e.preventDefault();
     email &&
     email.indexOf("@") > -1 &&
     onValidated ({
        EMAIL: email
     })
    }
    const clearFields = () => {
        setEmail('');
    }
   return(
    <section className="newsletter" id="newsletter">
        <div className="newsletter-bx">
         <Row>
          


           {/* {status === 'sending' && <Alert>Sending...</Alert>}
            {status === 'error' && <Alert variant="danger">{message}</Alert>}
            {status === 'success' && <Alert variant="success">{message}</Alert>}
          </Col> 
          <Col md={6} xl={7}>
            <form onSubmit={handleSubmit}>
              <div className="new-email-bx">
              <input value={email} type="email" onChange={(e) => setEmail(e.target.value)} placeholder="Email Address"/>
              <button type="submit">Submit</button>
              </div>
   </form>*/}
         
          <Col >
            <h3>Rreth nesh</h3>

            <TrackVisibility>
              {({ isVisible }) =>
               
            
<p ><span color="yellow"> <TrackVisibility>
              {({ isVisible }) =>
                <img className={isVisible ? "animate__animated animate__zoomIn" : ""} src={Logo} alt="Contact Us"/>
              }
            </TrackVisibility></span> Ashtu siç tregon simboli ynë, është kompani e besueshme, mbrojtëse, e kujdesshme. <br></br>E përkushtuar ndaj atyre që na mundëson ekzistencën, pra klientëve tanë.

Ju ofrojmë shërbime, që janë gjithmonë një hap përpara industrisë së sigurimeve në treg, duke gjetur zgjidhje të reja dhe të volitshme sigurimesh. Jemi të vetmit, që ofrojmë sigurimin Kasko, pavarësisht vlerës dhe vitit të prodhimit të veturës, jemi të vetmit  që ofrojmë sigurimin shtëpiak, pavarësisht madhësisë së shtëpisë, jemi të vetmit që ofrojmë shitjen online të Sigurimeve KASKO, të Pronës dhe të Shëndetit në Udhëtim, dhe jemi  të vetmit që ju bëjmë ofertën e sigurimit brenda 5 minutash.</p>
              }
            </TrackVisibility>
            </Col>
         </Row>
        </div>
        </section>
   ) 
}