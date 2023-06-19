import { Space, Table, Typography, Modal, Input, Button } from "antd";
import { useEffect, useState, useRef } from "react";
import { getAccident } from "../../API";
import { deleteAccident, updateAccident, addAccident } from "./delete";
import { EditOutlined, DeleteOutlined, PlusOutlined } from "@ant-design/icons";

function Accident() {
  const [loading, setLoading] = useState(false);
  const [dataSource, setDataSource] = useState([]);
  const [isEditing, setIsEditing] = useState(false);
  const [editingAccident, setEditingAccident] = useState(null);
  const [deleteModalVisible, setDeleteModalVisible] = useState(false);
  const [deletingAccident, setDeletingAccident] = useState(null);
  const [addModalVisible, setAddModalVisible] = useState(false);
  const [newAccident, setNewAccident] = useState({
    Proffesion: "",
    CoverageAmount: "",
    HealthConditions: "",
    Medications: "",
    StartDate: "",
    EndDate:"",
    Offer:""
  });

  const firstNameInputRef = useRef(null);
  useEffect(() => {
    setLoading(true);
    getAccident()
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
    setEditingAccident(record);
    setIsEditing(true);
  };

  const handleCancelEdit = () => {
    setIsEditing(false);
    setEditingAccident(null);
  };

  const handleSave = async () => {
    try {
      // Perform API call to update the user
      await updateAccident(editingAccident);
      // Update the dataSource with the edited user
      setDataSource((prevDataSource) =>
        prevDataSource.map((accident) =>
          accident.id === editingAccident.id ? editingAccident : accident
        )
      );
      // Reset editing state
      setIsEditing(false);
      setEditingAccident(null);
    } catch (error) {
      // Handle error
    }
  };

  const onDeleteAccident = (record) => {
    setDeletingAccident(record);
    setDeleteModalVisible(true);
  };

  const handleDeleteModalOk = async () => {
    try {
      await deleteAccident(deletingAccident.id);
      setDataSource((prevDataSource) =>
        prevDataSource.filter((accident) => accident.id !== deletingAccident.id)
      );
      setDeleteModalVisible(false);
      setDeletingAccident(null);
    } catch (error) {
      // Handle error
    }
  };

  const handleDeleteModalCancel = () => {
    setDeleteModalVisible(false);
    setDeletingAccident(null);
  };

  const handleAdd = () => {
    setAddModalVisible(true);
  };

  const handleAddModalOk = async () => {
    try {
      // Perform API call to add the user
      const addedAccident = await addAccident(newAccident);
      // Update the dataSource with the added user
      setDataSource((prevDataSource) => [...prevDataSource, addedAccident]);

      setAddModalVisible(false);
      setNewAccident({
        Proffesion: "",
    CoverageAmount: "",
    HealthConditions: "",
    Medications: "",
    StartDate: "",
    EndDate:"",
    Offer:"",
      });
    } catch (error) {
      // Handle error
    }
  };

  const handleAddModalCancel = () => {
    setAddModalVisible(false);
    setNewAccident({
      Proffesion: "",
      CoverageAmount: "",
      HealthConditions: "",
      Medications: "",
      StartDate: "",
      EndDate:"",
      Offer:""
    });
  };

  const columns = [
    {
      title: "Id",
      dataIndex: "id",
    },
    {
      title: "Proffesion",
      dataIndex: "Proffesion",
    },
    {
      title: "Coverage Amount",
      dataIndex: "CoverageAmount",
    },
    {
      title: "HealthConditions",
      dataIndex: "HealthConditions",
    },
    {
      title: "Medications",
      dataIndex: "Medications",
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
            onClick={() => onDeleteAccident(record)}
            style={{ color: "red", marginLeft: 12 }}
          />
        </>
      ),
    },
  ];

  return (
    <Space size={20} direction="vertical">
      <Typography.Title level={4}>Accidents</Typography.Title>
      <Button type="primary" icon={<PlusOutlined />} onClick={handleAdd}>
        Add Accident
      </Button>
      <Table
        loading={loading}
        columns={columns}
        dataSource={dataSource}
        pagination={{ pageSize: 5 }}
      />
      {isEditing && (
        <Modal
          title="Edit Accident"
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
            value={editingAccident?.Proffesion}
            onChange={(e) =>
              setEditingAccident((prevAccident) => ({
                ...prevAccident,
                Proffesion: e.target.value,
              }))
            }
          />
          <Input
            style={{ margin: "5px" }}
            value={editingAccident?.CoverageAmount}
            onChange={(e) =>
              setEditingAccident((prevAccident) => ({
                ...prevAccident,
                CoverageAmount: e.target.value,
              }))
            }
          />
          <Input
            style={{ margin: "5px" }}
            value={editingAccident?.HealthConditions}
            onChange={(e) =>
              setEditingAccident((prevAccident) => ({
                ...prevAccident,
                HealthConditions: e.target.value,
              }))
            }
          />
          <Input
            style={{ margin: "5px" }}
            value={editingAccident?.Medications}
            onChange={(e) =>
              setEditingAccident((prevAccident) => ({
                ...prevAccident,
               Medications: e.target.value,
              }))
            }
          />
          <Input
            style={{ margin: "5px" }}
            value={editingAccident?.StartDate}
            onChange={(e) =>
              setEditingAccident((prevAccident) => ({
                ...prevAccident,
                StartDate: e.target.value,
              }))
            }
          />
          <Input
            style={{ margin: "5px" }}
            value={editingAccident?.EndDate}
            onChange={(e) =>
              setEditingAccident((prevAccident) => ({
                ...prevAccident,
                
                  EndDate: e.target.value,
                
              }))
            }
          />
          <Input
            style={{ margin: "5px" }}
            value={editingAccident?.Offer}
            onChange={(e) =>
              setEditingAccident((prevAccident) => ({
                ...prevAccident,
                Offer: e.target.value,
              }))
            }
          />
        </Modal>
      )}
      <Modal
        title="Delete Accident"
        visible={deleteModalVisible}
        onOk={handleDeleteModalOk}
        onCancel={handleDeleteModalCancel}
        okText="Delete"
        cancelText="Cancel"
      >
        <p>Are you sure you want to delete this accident?</p>
      </Modal>
      <Modal
        title="Add Accident"
        visible={addModalVisible}
        onOk={handleAddModalOk}
        onCancel={handleAddModalCancel}
        okText="Add"
        cancelText="Cancel"
      >
        {/* Add input fields for adding a new user */}
        <Input
          style={{ margin: "5px" }}
          placeholder="Proffesion"
          value={newAccident.Proffesion}
          onChange={(e) =>
            setNewAccident((prevAccident) => ({
              ...prevAccident,
              Proffesion: e.target.value,
            }))
          }
        />
        <Input
          style={{ margin: "5px" }}
          placeholder="Coverage Amount"
          value={newAccident.CoverageAmount}
          onChange={(e) =>
            setNewAccident((prevAccident) => ({
              ...prevAccident,
              CoverageAmount: e.target.value,
            }))
          }
        />
        <Input
          style={{ margin: "5px" }}
          placeholder="Health Conditions"
          value={newAccident.HealthConditions}
          onChange={(e) =>
            setNewAccident((prevAccident) => ({
              ...prevAccident,
              HealthConditions: e.target.value,
            }))
          }
        />
        <Input
          style={{ margin: "5px" }}
          placeholder="Medications"
          value={newAccident.Medications}
          onChange={(e) =>
            setNewAccident((prevAccident) => ({
              ...prevAccident,
              Medications: e.target.value,
            }))
          }
        />
        <Input
          style={{ margin: "5px" }}
          placeholder="Start Date"
          value={newAccident.StartDate}
          onChange={(e) =>
            setNewAccident((prevAccident) => ({
              ...prevAccident,
              StartDate: e.target.value,
            }))
          }
        />
       <Input
          style={{ margin: "5px" }}
          placeholder="End Date"
          value={newAccident.EndDate}
          onChange={(e) =>
            setNewAccident((prevAccident) => ({
              ...prevAccident,
              EndDate: e.target.value,
            }))
          }
        />
        <Input
          style={{ margin: "5px" }}
          placeholder="Offer"
          value={newAccident.Offer}
          onChange={(e) =>
            setNewAccident((prevAccident) => ({
              ...prevAccident,
              Offer: e.target.value,
            }))
          }
        />
      </Modal>
    </Space>
  );
}

export default Accident;
