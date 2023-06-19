import { Space, Table, Typography, Modal, Input, Button } from "antd";
import { useEffect, useState, useRef } from "react";
import { getHealth } from "../../API";
import { deleteHealth, updateHealth, addHealth } from "./delete";
import { EditOutlined, DeleteOutlined, PlusOutlined } from "@ant-design/icons";

function Health() {
  const [loading, setLoading] = useState(false);
  const [dataSource, setDataSource] = useState([]);
  const [isEditing, setIsEditing] = useState(false);
  const [editingHealth, setEditingHealth] = useState(null);
  const [deleteModalVisible, setDeleteModalVisible] = useState(false);
  const [deletingHealth, setDeletingHealth] = useState(null);
  const [addModalVisible, setAddModalVisible] = useState(false);
  const [newHealth, setNewHealth] = useState({
    PaymentOption: "",
    StartDate: "",
    EndDate: "",
    Price: "",
    Offer: "",
   
     
  });

  const firstNameInputRef = useRef(null);
  useEffect(() => {
    setLoading(true);
    getHealth()
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
    setEditingHealth(record);
    setIsEditing(true);
  };

  const handleCancelEdit = () => {
    setIsEditing(false);
    setEditingHealth(null);
  };

  const handleSave = async () => {
    try {
      // Perform API call to update the user
      await updateHealth(editingHealth);
      // Update the dataSource with the edited user
      setDataSource((prevDataSource) =>
        prevDataSource.map((health) =>
          health.id === editingHealth.id ? editingHealth : health
        )
      );
      // Reset editing state
      setIsEditing(false);
      setEditingHealth(null);
    } catch (error) {
      // Handle error
    }
  };

  const onDeleteHealth = (record) => {
    setDeletingHealth(record);
    setDeleteModalVisible(true);
  };

  const handleDeleteModalOk = async () => {
    try {
      await deleteHealth(deletingHealth.id);
      setDataSource((prevDataSource) =>
        prevDataSource.filter((health) => health.id !== deletingHealth.id)
      );
      setDeleteModalVisible(false);
      setDeletingHealth(null);
    } catch (error) {
      // Handle error
    }
  };

  const handleDeleteModalCancel = () => {
    setDeleteModalVisible(false);
    setDeletingHealth(null);
  };

  const handleAdd = () => {
    setAddModalVisible(true);
  };

  const handleAddModalOk = async () => {
    try {
      // Perform API call to add the user
      const addedHealth = await addHealth(newHealth);
      // Update the dataSource with the added user
      setDataSource((prevDataSource) => [...prevDataSource, addedHealth]);

      setAddModalVisible(false);
      setNewHealth({
        PaymentOption: "",
    StartDate: "",
    EndDate: "",
    Price: "",
    Offer: "",
      });
    } catch (error) {
      // Handle error
    }
  };

  const handleAddModalCancel = () => {
    setAddModalVisible(false);
    setNewHealth({
      PaymentOption: "",
      StartDate: "",
      EndDate: "",
      Price: "",
      Offer: "",
    });
  };

  const columns = [
    {
      title: "Id",
      dataIndex: "id",
    },
    {
      title: "Payment Options",
      dataIndex: "PaymentOption",
    },
    {
      title: "Start Date",
      dataIndex: "StartDate",
    },
    {
      title: "End Date",
      dataIndex: "EndDate",
    },
    {
      title: "Price",
      dataIndex: "Price",
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
            onClick={() => onDeleteHealth(record)}
            style={{ color: "red", marginLeft: 12 }}
          />
        </>
      ),
    },
  ];

  return (
    <Space size={20} direction="vertical">
      <Typography.Title level={4}>Health Insurance</Typography.Title>
      <Button type="primary" icon={<PlusOutlined />} onClick={handleAdd}>
        Add Health Insurance
      </Button>
      <Table
        loading={loading}
        columns={columns}
        dataSource={dataSource}
        pagination={{ pageSize: 5 }}
      />
      {isEditing && (
        <Modal
          title="Edit Health Insurance"
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
            value={editingHealth?.PaymentOption}
            onChange={(e) =>
              setEditingHealth((prevHealth) => ({
                ...prevHealth,
                PaymentOption: e.target.value,
              }))
            }
          />
          <Input
            style={{ margin: "5px" }}
            value={editingHealth?.StartDate}
            onChange={(e) =>
              setEditingHealth((prevHealth) => ({
                ...prevHealth,
                StartDate: e.target.value,
              }))
            }
            />
            <Input
            style={{ margin: "5px" }}
            value={editingHealth?.EndDate}
            onChange={(e) =>
              setEditingHealth((prevHealth) => ({
                ...prevHealth,
                EndDate: e.target.value,
              }))
            }
          />
         <Input
            style={{ margin: "5px" }}
            value={editingHealth?.Price}
            onChange={(e) =>
              setEditingHealth((prevHealth) => ({
                ...prevHealth,
                Price: e.target.value,
              }))
            }
          />
         <Input
            style={{ margin: "5px" }}
            value={editingHealth?.Offer}
            onChange={(e) =>
              setEditingHealth((prevHealth) => ({
                ...prevHealth,
                Offer: e.target.value,
              }))
            }
          />
          
        </Modal>
      )}
      <Modal
        title="Delete Health Insurance"
        visible={deleteModalVisible}
        onOk={handleDeleteModalOk}
        onCancel={handleDeleteModalCancel}
        okText="Delete"
        cancelText="Cancel"
      >
        <p>Are you sure you want to delete this health insurance?</p>
      </Modal>
      <Modal
        title="Add Health Insurance"
        visible={addModalVisible}
        onOk={handleAddModalOk}
        onCancel={handleAddModalCancel}
        okText="Add"
        cancelText="Cancel"
      >
        {/* Add input fields for adding a new user */}
        <Input
          style={{ margin: "5px" }}
          placeholder="Payment Options"
          value={newHealth.PaymentOption}
          onChange={(e) =>
            setNewHealth((prevHealth) => ({
              ...prevHealth,
              PaymentOption: e.target.value,
            }))
          }
        />
         <Input
          style={{ margin: "5px" }}
          placeholder="Start Date"
          value={newHealth.StartDate}
          onChange={(e) =>
            setNewHealth((prevHealth) => ({
              ...prevHealth,
              StartDate: e.target.value,
            }))
          }
        />
        <Input
          style={{ margin: "5px" }}
          placeholder="End Date"
          value={newHealth.EndDate}
          onChange={(e) =>
            setNewHealth((prevHealth) => ({
              ...prevHealth,
              EndDate: e.target.value,
            }))
          }
        />
        <Input
          style={{ margin: "5px" }}
          placeholder="Price"
          value={newHealth.Price}
          onChange={(e) =>
            setNewHealth((prevHealth) => ({
              ...prevHealth,
              Price: e.target.value,
            }))
          }
        />
        <Input
          style={{ margin: "5px" }}
          placeholder="Offer"
          value={newHealth.Offer}
          onChange={(e) =>
            setNewHealth((prevHealth) => ({
              ...prevHealth,
             Offer: e.target.value,
            }))
          }
        />
       
      </Modal>
    </Space>
  );
}

export default Health;
