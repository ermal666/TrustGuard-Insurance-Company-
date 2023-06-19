import { Space, Table, Typography, Modal, Input, Button } from "antd";
import { useEffect, useState, useRef } from "react";
import { getCasco } from "../../API";
import { deleteCasco, updateCasco, addCasco } from "./delete";
import { EditOutlined, DeleteOutlined, PlusOutlined } from "@ant-design/icons";

function Casco() {
  const [loading, setLoading] = useState(false);
  const [dataSource, setDataSource] = useState([]);
  const [isEditing, setIsEditing] = useState(false);
  const [editingCasco, setEditingCasco] = useState(null);
  const [deleteModalVisible, setDeleteModalVisible] = useState(false);
  const [deletingCasco, setDeletingCasco] = useState(null);
  const [addModalVisible, setAddModalVisible] = useState(false);
  const [newCasco, setNewCasco] = useState({
    VinNumber: "",
    CarModel: "",
     Plate: "",
     Producer:"",
    Color: "",
    EngineCapacity: "",
    SeatingCapacity: "",
    PurchaseDate:"",
    Offer:""
  });

  const firstNameInputRef = useRef(null);
  useEffect(() => {
    setLoading(true);
    getCasco()
      .then((res) => {
        setDataSource(res.users);
        setLoading(false);
      })
      .catch((error) => {
        // Handle error
        setLoading(false);
      });
  }, []);

  const handleEdit = (record) => {
    setEditingCasco(record);
    setIsEditing(true);
  };

  const handleCancelEdit = () => {
    setIsEditing(false);
    setEditingCasco(null);
  };

  const handleSave = async () => {
    try {
      // Perform API call to update the user
      await updateCasco(editingCasco);
      // Update the dataSource with the edited user
      setDataSource((prevDataSource) =>
        prevDataSource.map((casco) =>
          casco.id === editingCasco.id ? editingCasco : casco
        )
      );
      // Reset editing state
      setIsEditing(false);
      setEditingCasco(null);
    } catch (error) {
      // Handle error
    }
  };

  const onDeleteCasco = (record) => {
    setDeletingCasco(record);
    setDeleteModalVisible(true);
  };

  const handleDeleteModalOk = async () => {
    try {
      await deleteCasco(deletingCasco.id);
      setDataSource((prevDataSource) =>
        prevDataSource.filter((casco) => casco.id !== deletingCasco.id)
      );
      setDeleteModalVisible(false);
      setDeletingCasco(null);
    } catch (error) {
      // Handle error
    }
  };

  const handleDeleteModalCancel = () => {
    setDeleteModalVisible(false);
    setDeletingCasco(null);
  };

  const handleAdd = () => {
    setAddModalVisible(true);
  };

  const handleAddModalOk = async () => {
    try {
      // Perform API call to add the user
      const addedCasco = await addCasco(newCasco);
      // Update the dataSource with the added user
      setDataSource((prevDataSource) => [...prevDataSource, addedCasco]);

      setAddModalVisible(false);
      setNewCasco({
        VinNumber: "",
        CarModel: "",
         Plate: "",
         Producer:"",
        Color: "",
        EngineCapacity: "",
        SeatingCapacity: "",
        PurchaseDate:"",
        Offer:""
      });
    } catch (error) {
      // Handle error
    }
  };

  const handleAddModalCancel = () => {
    setAddModalVisible(false);
    setNewCasco({
      VinNumber: "",
    CarModel: "",
     Plate: "",
     Producer:"",
    Color: "",
    EngineCapacity: "",
    SeatingCapacity: "",
    PurchaseDate:"",
    Offer:""
    });
  };

  const columns = [
    {
      title: "Id",
      dataIndex: "Id",
    },
    {
      title: "Vin Number",
      dataIndex: "VinNumber",
    },
    {
      title: "Car Model",
      dataIndex: "CarModel",
    },
    {
      title: "Plate",
      dataIndex: "Plate",
    },
    {
      title: "Producer",
      dataIndex: "Producer",
    },
    {
      title: "Color",
      dataIndex: "Color",
    },
    {
      title: "Engine Capacity",
      dataIndex: "EngineCapacity",
    },
    {
      title: "Seating Capacity",
      dataIndex: "SeatingCapacity",
    },
    {
      title: "Purchase Date",
      dataIndex: "PurchaseDate",
    },
    {
      title: "Offer",
      dataIndex: "Offer",
      
    },
    {
      key: "actions",
      title: "Actions",
      render: (record) => (
        <>
          <EditOutlined onClick={() => handleEdit(record)} />
          <DeleteOutlined
            onClick={() => onDeleteCasco(record)}
            style={{ color: "red", marginLeft: 12 }}
          />
        </>
      ),
    },
  ];

  return (
    <Space size={20} direction="vertical">
      <Typography.Title level={4}>Cascos</Typography.Title>
      <Button type="primary" icon={<PlusOutlined />} onClick={handleAdd}>
        Add Casco
      </Button>
      <Table
        loading={loading}
        columns={columns}
        dataSource={dataSource}
        pagination={{ pageSize: 5 }}
      />
      {isEditing && (
        <Modal
          title="Edit Casco"
          visible={isEditing}
          onCancel={handleCancelEdit}
          footer={[
            <Button key="cancel" onClick={handleCancelEdit}>
              Cancel
            </Button>,
            <Button key="save" type="primary" onClick={handleSave}>
              Save
            </Button>,
          ]}
        >
          {/* Add input fields for editing user details */}
          <Input
            style={{ margin: "5px" }}
            value={editingCasco?.VinNumber}
            onChange={(e) =>
              setEditingCasco((prevCasco) => ({
                ...prevCasco,
                VinNumber: e.target.value,
              }))
            }
          />
          <Input
            style={{ margin: "5px" }}
            value={editingCasco?.CarModel}
            onChange={(e) =>
              setEditingCasco((prevCasco) => ({
                ...prevCasco,
                CarModel: e.target.value,
              }))
            }
          />
          <Input
            style={{ margin: "5px" }}
            value={editingCasco?.Plate}
            onChange={(e) =>
              setEditingCasco((prevCasco) => ({
                ...prevCasco,
                Plate: e.target.value,
              }))
            }
          />
          <Input
            style={{ margin: "5px" }}
            value={editingCasco?.Producer}
            onChange={(e) =>
              setEditingCasco((prevCasco) => ({
                ...prevCasco,
               Producer: e.target.value,
              }))
            }
          />
          <Input
            style={{ margin: "5px" }}
            value={editingCasco?.Color}
            onChange={(e) =>
              setEditingCasco((prevCasco) => ({
                ...prevCasco,
                Color: e.target.value,
              }))
            }
          />
           <Input
            style={{ margin: "5px" }}
            value={editingCasco?.EngineCapacity}
            onChange={(e) =>
              setEditingCasco((prevCasco) => ({
                ...prevCasco,
                EngineCapacity: e.target.value,
              }))
            }
          />
           <Input
            style={{ margin: "5px" }}
            value={editingCasco?.SeatingCapacity}
            onChange={(e) =>
              setEditingCasco((prevCasco) => ({
                ...prevCasco,
                SeatingCapacity: e.target.value,
              }))
            }
          />
          <Input
            style={{ margin: "5px" }}
            value={editingCasco?.PurchaseDate}
            onChange={(e) =>
              setEditingCasco((prevCasco) => ({
                ...prevCasco,
                
                  PurchaseDate: e.target.value,
                
              }))
            }
          />
          <Input
            style={{ margin: "5px" }}
            value={editingCasco?.Offer}
            onChange={(e) =>
              setEditingCasco((prevCasco) => ({
                ...prevCasco,
                Offer: e.target.value,
              }))
            }
          />
        </Modal>
      )}
      <Modal
        title="Delete Casco"
        visible={deleteModalVisible}
        onOk={handleDeleteModalOk}
        onCancel={handleDeleteModalCancel}
        okText="Delete"
        cancelText="Cancel"
      >
        <p>Are you sure you want to delete this casco?</p>
      </Modal>
      <Modal
        title="Add Casco"
        visible={addModalVisible}
        onOk={handleAddModalOk}
        onCancel={handleAddModalCancel}
        okText="Add"
        cancelText="Cancel"
      >
        {/* Add input fields for adding a new user */}
        <Input
          style={{ margin: "5px" }}
          placeholder="Vin Number"
          value={newCasco.VinNumber}
          onChange={(e) =>
            setNewCasco((prevCasco) => ({
              ...prevCasco,
              VinNumber: e.target.value,
            }))
          }
        />
        <Input
          style={{ margin: "5px" }}
          placeholder="Car Mododel"
          value={newCasco.CarModel}
          onChange={(e) =>
            setNewCasco((prevCasco) => ({
              ...prevCasco,
              CarModel: e.target.value,
            }))
          }
        />
        <Input
          style={{ margin: "5px" }}
          placeholder="Plate"
          value={newCasco.Plate}
          onChange={(e) =>
            setNewCasco((prevCasco) => ({
              ...prevCasco,
              Plate: e.target.value,
            }))
          }
        />
         <Input
          style={{ margin: "5px" }}
          placeholder="Producer"
          value={newCasco.Producer}
          onChange={(e) =>
            setNewCasco((prevCasco) => ({
              ...prevCasco,
              Producer: e.target.value,
            }))
          }
        />
        <Input
          style={{ margin: "5px" }}
          placeholder="Color"
          value={newCasco.Color}
          onChange={(e) =>
            setNewCasco((prevCasco) => ({
              ...prevCasco,
              Color: e.target.value,
            }))
          }
        />
        <Input
          style={{ margin: "5px" }}
          placeholder="Engine Capacity"
          value={newCasco.EngineCapacity}
          onChange={(e) =>
            setNewCasco((prevCasco) => ({
              ...prevCasco,
              EngineCapacity: e.target.value,
            }))
          }
        />
        <Input
          style={{ margin: "5px" }}
          placeholder="Seating Capacity"
          value={newCasco.SeatingCapacity}
          onChange={(e) =>
            setNewCasco((prevCasco) => ({
              ...prevCasco,
              SeatingCapacity: e.target.value,
            }))
          }
        />
        <Input
          style={{ margin: "5px" }}
          placeholder="Purchase Date"
          value={newCasco.PurchaseDate}
          onChange={(e) =>
            setNewCasco((prevCasco) => ({
              ...prevCasco,
              PurchaseDate: e.target.value,
            }))
          }
        />
        <Input
          style={{ margin: "5px" }}
          placeholder="Offer"
          value={newCasco.Offer}
          onChange={(e) =>
            setNewCasco((prevCasco) => ({
              ...prevCasco,
              Offer: e.target.value,
            }))
          }
        />
      </Modal>
    </Space>
  );
}

export default Casco;
