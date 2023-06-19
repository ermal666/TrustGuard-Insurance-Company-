import { ApartmentOutlined, CarFilled, CarOutlined, HomeFilled, MedicineBoxFilled,  MonitorOutlined,  ToolOutlined } from "@ant-design/icons";
import {Card, Space, Statistic, Table, Typography } from "antd";
import { useEffect, useState } from "react";
import { getUsers, getAccident, getApartment, getCasco,getHealth, getHome, getTpl, getOffer} from "../../API";


function Dashboard (){
  const [casco, setCasco] = useState(0);
  const [tpl, setTpl] = useState(0);
  const [home, setHome] = useState(0);
  const [apartment, setApartment] = useState(0);
  const [health, setHealth] = useState(0);
  const [accident, setAccident] = useState(0);
  const [offer, setOffer] = useState(0);

  useEffect(() => {
    getCasco().then((res) => {
      setCasco(res.total);
     
    });
    getTpl().then((res) => {
      setTpl(res.total);
    });
    getHome().then((res) => {
      setHome(res.total);
    });
    getApartment().then((res) => {
      setApartment(res.total);
     
    });
    getHealth().then((res) => {
      setHealth(res.total);
    });
    
    getAccident().then((res) => {
      setAccident(res.total);
    });
    getOffer().then((res) => {
      setOffer(res.price);
    });
  }, []);
    return(
        
          <Space size={40}  direction="vertical">
<Typography.Title level={8}>Dashboard</Typography.Title>
<Space style={{marginLeft:"48px"}} direction="horizonal">


 <DashboardCard  icon={ <CarFilled  
              style={{
                color: "blue",
                backgroundColor: "rgba(0,0,255,0.25)",
                borderRadius: 20,
                fontSize: 34,
                padding: 10,
                margin:10,
              }}
              /> 
              } title={"Casco"} value={casco}/>
 <DashboardCard icon={ <CarOutlined
  style={{
    color: "blue",
    backgroundColor: "rgba(0,0,255,0.25)",
    borderRadius: 20,
    fontSize: 34,
    padding: 10,
    margin:10,
  }}
  /> 
  } title={"TPL"} value={tpl}/>
 <DashboardCard icon={ <HomeFilled
  style={{
    color: "green",
    backgroundColor: "rgba(0,255,0,0.25)",
    borderRadius: 20,
    fontSize: 34,
    padding: 10,
    margin:10,
  }}
 /> 
 } title={"Home"} value={home}/>
 <DashboardCard icon={ <ApartmentOutlined
  style={{
    color: "green",
    backgroundColor: "rgba(0,255,0,0.25)",
    borderRadius: 20,
    fontSize: 34,
    padding: 10,
    margin:10,
  }}
 />
  } title={"Apartment"} value={apartment}/>
 <DashboardCard icon={ <MedicineBoxFilled
  style={{
    color: "red",
    backgroundColor: "rgba(255,0,0,0.25)",
    borderRadius: 20,
    fontSize: 34,
    padding: 10,
    margin:10,
  }}
 /> 
 } title={"Health"} value={health}/>

 <DashboardCard icon={ <ToolOutlined
  style={{
    color: "red",
    backgroundColor: "rgba(255,0,0,0.25)",
    borderRadius: 20,
    fontSize: 34,
    padding: 10,
    margin:10,
  }}
 /> 
 } title={"Accident"} value={accident}/>
  <DashboardCard icon={ <MonitorOutlined
  style={{
    color: "purple",
    backgroundColor: "rgba(0,255,255,0.25)",
    borderRadius: 20,
    fontSize: 34,
    padding: 10,
    margin:10,

  }}
 /> 
 } title={"Offer"} value={offer}/>

</Space>
<Space>
  <RecentUsers/>

</Space>
</Space>

    );
}
function DashboardCard({title, value, icon}){
return(
    <Card>
    <Space direction="horizonal">
        {icon}
   
     <Statistic title={title} value={value}/>  
     </Space>
    </Card>
);
}
function RecentUsers() {
  const [dataSource, setDataSource] = useState([]);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    setLoading(true);
    getUsers().then((res) => {
      setDataSource(res.users.splice(0, 3));
      setLoading(false);
    });
  }, []);

  return (
    <div >
      <Typography.Title level={4} style={{ marginLeft:"600px" }} >Recent Users</Typography.Title>
      <Table style={{marginLeft:"380px", marginTop:"20px"}}
        columns={[
          
          {
            title: "Full Name",
            dataIndex: "FullName",
          },
          {
            title: "Birthday",
            dataIndex: "BirthDate",
          },
         
          {
            title: "Phone",
            dataIndex: "PhoneNumber",
          },
          {
            title: "Address",
            dataIndex: "Address",
            
          },
        ]}
        loading={loading}
        dataSource={dataSource}
        pagination={false}
      ></Table>
    </div>
  );
}

export default Dashboard;