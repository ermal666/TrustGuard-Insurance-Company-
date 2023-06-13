import { Col, Container, Row, Nav, Tab } from "react-bootstrap";
import { BlogCard } from "./BlogCard";
import colorSharp2 from "../assets/img/color-sharp2.png";
import blImg1 from "../assets/img/blog-1-portrait.jpg";
import blImg2 from "../assets/img/blog-2-portrait.jpg";
import blImg3 from "../assets/img/blog-2-portrait.jpg";
import "animate.css";
import TrackVisibility from "react-on-screen";


export const Blogs = () => {
    const blogs = [
        {
            title: "Masat parandaluese per COVID-19",
            description: "Design & Development",
            imgUrl: blImg1,
          },
          {
            title: "sigurimi i detyrueshem nga autopergjegjesia ne kohen e pandemise",
            description: "Design & Development",
            imgUrl: blImg2,
          },
          {
            title: "Masat parandaluese per COVID-19",
            description: "Design & Development",
            imgUrl: blImg3,
          },
         

    ];
    return (
        <section className="blog" id="blog">
        <Container>
            <Row>
                <Col>
                <TrackVisibility>
        {({ isVisible }) =>
        <div className={isVisible ? "animate__animated animate__slideInUp" : ""}>
                <h2>Blogu</h2>
                <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.</p>
                </div>}
                </TrackVisibility>
                <Tab.Container id="blogs-tabs" defaultActiveKey="first">
                  <Nav variant="pills" className="nav-pills mb-5 justify-content-center align-items-center" id="pills-tab">
                    <Nav.Item>
                      <Nav.Link eventKey="first">Tab 1</Nav.Link>
                    </Nav.Item>
                    <Nav.Item>
                      <Nav.Link eventKey="second">Tab 2</Nav.Link>
                    </Nav.Item>
                    <Nav.Item>
                      <Nav.Link eventKey="third">Tab 3</Nav.Link>
                    </Nav.Item>
                  </Nav>
                  <Tab.Content >
                    <Tab.Pane eventKey="first">
                      <Row>
                        {
                          blogs.map((blog, index) => {
                            return (
                              <BlogCard
                                key={index}
                                {...blog}
                                />
                            )
                          })
                        }
                      </Row>
                    </Tab.Pane>
                    <Tab.Pane eventKey="section">
                      <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Cumque quam, quod neque provident velit, rem explicabo excepturi id illo molestiae blanditiis, eligendi dicta officiis asperiores delectus quasi inventore debitis quo.</p>
                    </Tab.Pane>
                    <Tab.Pane eventKey="third">
                      <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Cumque quam, quod neque provident velit, rem explicabo excepturi id illo molestiae blanditiis, eligendi dicta officiis asperiores delectus quasi inventore debitis quo.</p>
                    </Tab.Pane>
                  </Tab.Content>
                </Tab.Container>
                </Col>
            </Row>
        </Container>
        <img className="background-image-right" src={colorSharp2}></img>
        </section>

    )
}