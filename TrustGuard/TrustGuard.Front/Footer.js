import { Container, Row , Col} from "react-bootstrap";
import { MailchimpForm } from "./MailchimpForm";
import logo from "../assets/img/logo.png";
import facebook from "../assets/img/facebook.png";
import instagram from "../assets/img/instagram.png";
import whatsapp from "../assets/img/whatsapp.png";

export const Footer = () => {
    return (
      <footer className="footer">
      <Container>
      <Row className="align-item-center">
      <MailchimpForm />
      <Col sm={6}>
      <img src={logo} alt="Logo" />
      </Col>
      <Col sm={6} className="text-center text-sm-end">
      <div className="social-icon">
      <a href=""><img src={facebook}/></a>
      <a href=""><img src={instagram}/></a>
      <a href=""><img src={whatsapp}/></a>
      </div>
      <p>CopyRight 2023. All Right Reserved</p>
      </Col>
      </Row>
      </Container>
      </footer>  
    )
}