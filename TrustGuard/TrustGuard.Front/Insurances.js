import { Container, Row, Col } from "react-bootstrap";
import Carousel from "react-multi-carousel";
import 'react-multi-carousel/lib/styles.css';
import meter1 from "../assets/img/personal.svg";
import meter2 from "../assets/img/vehicle.svg";
import meter3 from "../assets/img/estate.svg";
import meter4 from "../assets/img/guaranty.svg";
import colorSharp from "../assets/img/color-sharp.png";
import TrackVisibility from "react-on-screen";
import "animate.css";
import { useNavigate } from "react-router-dom";

export const Insurances = () => {
  
    const responsive = {
        superLargeDesktop: {
          // the naming can be any, depends on you.
          breakpoint: { max: 4000, min: 3000 },
          items: 5
        },
        desktop: {
          breakpoint: { max: 3000, min: 1024 },
          items: 3
        },
        tablet: {
          breakpoint: { max: 1024, min: 464 },
          items: 2
        },
        mobile: {
          breakpoint: { max: 464, min: 0 },
          items: 1
        }
    };
    const navigate = useNavigate();
    return (
      
      <section className="insurance" id="insurances">
      <Container>
        <Row>
        <Col>
        <div className="insurance-bx">
         <h2>
          Sigurimet e ofruara:
         </h2>
         <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry.<br></br> Lorem Ipsum has been the industry's standard dummy text.</p>
         <Carousel responsive={responsive} infinite={true} className="insurance-slider">
          <div className="item">
          <img src={meter1} alt="Image"/>
          
          <h5>Individual</h5>
          <TrackVisibility>
        {({ isVisible }) =>
        <div className={isVisible ? "animate__animated animate__slideInRight" : ""}>
          <p>Shëndet <br></br>Udhëtim <br></br>Aksident <br></br>Lejeqëndrim</p>
          <button onClick={() => navigate('/individualinsurance')}>Go to About</button>
          </div>}
          </TrackVisibility>
          </div>
          <div className="item">
          <img src={meter2} alt="Image"/>
          <h5>Automjet</h5>
          <TrackVisibility>
        {({ isVisible }) =>
        <div className={isVisible ? "animate__animated animate__slideInRight" : ""}>
          <p>Sigurimi i automjetit<br></br>TPL+<br></br>Kasko</p>
          
          </div>}
          </TrackVisibility>
          </div>
          <div className="item">
          <img src={meter3} alt="Image"/>
          <h5>Pronë</h5>
          <p>Biznes<br></br>Shtëpi<br></br>Banesë</p>
          </div>
          <div className="item">
          <img src={meter4} alt="Image"/>
          <h5>Garancioni</h5>
          <TrackVisibility>
        {({ isVisible }) =>
        <div className={isVisible ? "animate__animated animate__slideInRight" : ""}>
          <p>Tender <br></br>Ekzekutim/Mirëmbajtje<br></br>Doganore/Avans</p>
          </div>}
          </TrackVisibility>
          </div>
         </Carousel>
        </div>
        </Col>  
        </Row>
      </Container>
     {/* <img className="backgorund-image-left" src={colorSharp} />*/}
      </section>
      
    );

}