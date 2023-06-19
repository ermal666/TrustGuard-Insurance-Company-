import { Space, Table, Typography, Modal, Input, Button } from "antd";
import { useEffect, useState, useRef } from "react";
import { getOffer } from "../../API";
import { deleteOffer, updateOffer, addOffer } from "./delete";
import { EditOutlined, DeleteOutlined, PlusOutlined } from "@ant-design/icons";

function Offer() {
  const [loading, setLoading] = useState(false);
  const [dataSource, setDataSource] = useState([]);
  const [isEditing, setIsEditing] = useState(false);
  const [editingOffer, setEditingOffer] = useState(null);
  const [deleteModalVisible, setDeleteModalVisible] = useState(false);
  const [deletingOffer, setDeletingOffer] = useState(null);
  const [addModalVisible, setAddModalVisible] = useState(false);
  const [newOffer, setNewOffer] = useState({
    
   Name: "",
   Price:"",
   InsuranceId:"",
 
     
  });

  const firstNameInputRef = useRef(null);
  useEffect(() => {
    setLoading(true);
    getOffer()
      .then((res) => {
        setDataSource(res.products);
        setLoading(false);
      })
      .catch((error) => {
        // Handle error
        setLoading(false);
      });
  }, []);

  const handleEdit = (record) => {
    setEditingOffer(record);
    setIsEditing(true);
  };

  const handleCancelEdit = () => {
    setIsEditing(false);
    setEditingOffer(null);
  };

  const handleSave = async () => {
    try {
      // Perform API call to update the user
      await updateOffer(editingOffer);
      // Update the dataSource with the edited user
      setDataSource((prevDataSource) =>
        prevDataSource.map((offer) =>
          offer.id === editingOffer.id ? editingOffer : offer
        )
      );
      // Reset editing state
      setIsEditing(false);
      setEditingOffer(null);
    } catch (error) {
      // Handle error
    }
  };

  const onDeleteOffer = (record) => {
    setDeletingOffer(record);
    setDeleteModalVisible(true);
  };

  const handleDeleteModalOk = async () => {
    try {
      await deleteOffer(deletingOffer.id);
      setDataSource((prevDataSource) =>
        prevDataSource.filter((offer) => offer.id !== deletingOffer.id)
      );
      setDeleteModalVisible(false);
      setDeletingOffer(null);
    } catch (error) {
      // Handle error
    }
  };

  const handleDeleteModalCancel = () => {
    setDeleteModalVisible(false);
    setDeletingOffer(null);
  };

  const handleAdd = () => {
    setAddModalVisible(true);
  };

  const handleAddModalOk = async () => {
    try {
      // Perform API call to add the user
      const addedOffer = await addOffer(newOffer);
      // Update the dataSource with the added user
      setDataSource((prevDataSource) => [...prevDataSource, addedOffer]);

      setAddModalVisible(false);
      setNewOffer({
        Nameame: "",
         Price:"",
         InsuranceId:"",
      });
    } catch (error) {
      // Handle error
    }
  };

  const handleAddModalCancel = () => {
    setAddModalVisible(false);
    setNewOffer({
      Name: "",
      Price:"",
      InsuranceId:"",
    });
  };

  const columns = [
    {
        title: "Id",
        dataIndex: "id",
      },
    {
      title: "Offer Name",
      dataIndex: "Name",
    },
    {
      title: "Price",
      dataIndex: "Price",
    },
    {
      title: "Insurance type",
      dataIndex: "InsuranceId",
    },
   
    {
      key: "actions",
      title: "Actions",
      render: (record) => (
        <>
          <EditOutlined onClick={() => handleEdit(record)} />
          <DeleteOutlined
            onClick={() => onDeleteOffer(record)}
            style={{ color: "red", marginLeft: 12 }}
          />
        </>
      ),
    },
  ];

  return (
    <Space size={20} direction="vertical">
      <Typography.Title level={4}>Offer</Typography.Title>
      <Button type="primary" icon={<PlusOutlined />} onClick={handleAdd}>
        Add Offer
      </Button>
      <Table
        loading={loading}
        columns={columns}
        dataSource={dataSource}
        pagination={{ pageSize: 5 }}
      />
      {isEditing && (
        <Modal
          title="Edit Offer"
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
            value={editingOffer?.Name}
            onChange={(e) =>
              setEditingOffer((prevOffer) => ({
                ...prevOffer,
                Name: e.target.value,
              }))
            }
          />
          <Input
            style={{ margin: "5px" }}
            value={editingOffer?.Price}
            onChange={(e) =>
              setEditingOffer((prevOffer) => ({
                ...prevOffer,
                Price: e.target.value,
              }))
            }
            />
            <Input
            style={{ margin: "5px" }}
            value={editingOffer?.InsuranceId}
            onChange={(e) =>
              setEditingOffer((prevOffer) => ({
                ...prevOffer,
                InsuranceId: e.target.value,
              }))
            }
          />
        
          
        </Modal>
      )}
      <Modal
        title="Delete Offer"
        visible={deleteModalVisible}
        onOk={handleDeleteModalOk}
        onCancel={handleDeleteModalCancel}
        okText="Delete"
        cancelText="Cancel"
      >
        <p>Are you sure you want to delete this Offer?</p>
      </Modal>
      <Modal
        title="Add Offer"
        visible={addModalVisible}
        onOk={handleAddModalOk}
        onCancel={handleAddModalCancel}
        okText="Add"
        cancelText="Cancel"
      >
        {/* Add input fields for adding a new user */}
        <Input
          style={{ margin: "5px" }}
          placeholder="Offer Name"
          value={newOffer.Name}
          onChange={(e) =>
            setNewOffer((prevOffer) => ({
              ...prevOffer,
             Name: e.target.value,
            }))
          }
        />
         <Input
          style={{ margin: "5px" }}
          placeholder="Price"
          value={newOffer.Price}
          onChange={(e) =>
            setNewOffer((prevOffer) => ({
              ...prevOffer,
             Price: e.target.value,
            }))
          }
        />
         <Input
          style={{ margin: "5px" }}
          placeholder="Insurance type"
          value={newOffer.InsuranceId}
          onChange={(e) =>
            setNewOffer((prevOffer) => ({
              ...prevOffer,
             InsuranceId: e.target.value,
            }))
          }
        />
         </Modal>
    </Space>
  );
}

export default Offer;
