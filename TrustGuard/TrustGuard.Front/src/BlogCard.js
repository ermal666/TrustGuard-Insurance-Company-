import { Col } from "react-bootstrap";

export const BlogCard = ({ title, description, imgUrl }) => {
  return (
    <Col size={12} sm={6} md={4}>
      <div className="bl-imgbx">
        <img src={imgUrl} />
        <div className="bl-txtx">
          <h4>{title}</h4>
          <span>{description}</span>
        </div>
      </div>
    </Col>
  )
}