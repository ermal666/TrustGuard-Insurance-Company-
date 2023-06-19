
import { ApartmentOutlined, AppstoreOutlined, CarFilled, CarOutlined,  HomeFilled, MedicineBoxFilled,  MonitorOutlined,  ToolOutlined, UserAddOutlined } from "@ant-design/icons";
import { Menu } from "antd";
import { useNavigate, useLocation } from "react-router-dom";
import { useEffect, useState } from "react";

function SideMenu () {
  const location = useLocation();
  const [selectedKeys, setSelectedKeys] = useState("/");

  useEffect(() => {
    const pathName = location.pathname;
    setSelectedKeys(pathName);
  }, [location.pathname]);

  const navigate = useNavigate();
    return ( <div className="SideMenu">
     <Menu 
        className="SideMenuVertical"
        mode="vertical"
        onClick={(item) => {
          //item.key
          navigate(item.key);
        }}
        selectedKeys={[selectedKeys]}
        items={[
          {
          label:"Dashboard",
          icon:<AppstoreOutlined/>,
          key:'/',
     },
     {
      label:"Users",
      icon:<UserAddOutlined/>,
      key:'/users',
 },
     {
      label:"TPL",
      icon:<CarOutlined/>,
      key:'/tpl',
 },
 {
  label:"Casco",
  icon:<CarFilled/>,
  key:'/casco',
},
{
  label:"Home",
  icon:<HomeFilled/>,
  key:'/home',
},
{
  label:"Apartment",
  icon:<ApartmentOutlined/>,
  key:'/apartment',
},
{
  label:"Health",
  icon:<MedicineBoxFilled/>,
  key:'/health',
},

{
  label:"Accident",
  icon:<ToolOutlined/>,
  key:'/accident',
},
{
  label:"Offer",
  icon:<MonitorOutlined/>,
  key:'/offer',
},
     ]}>

     </Menu>
  </div>
    );
  }
  export default SideMenu;