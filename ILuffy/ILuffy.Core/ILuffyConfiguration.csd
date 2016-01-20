<?xml version="1.0" encoding="utf-8"?>
<configurationSectionModel xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="dee4848e-76d3-4a77-b8d8-e6706f84d49f" namespace="ILuffy.Core" xmlSchemaNamespace="urn:ILuffy.Core" xmlns="http://schemas.microsoft.com/dsltools/ConfigurationSectionDesigner">
  <typeDefinitions>
    <externalType name="String" namespace="System" />
    <externalType name="Boolean" namespace="System" />
    <externalType name="Int32" namespace="System" />
    <externalType name="Int64" namespace="System" />
    <externalType name="Single" namespace="System" />
    <externalType name="Double" namespace="System" />
    <externalType name="DateTime" namespace="System" />
    <externalType name="TimeSpan" namespace="System" />
  </typeDefinitions>
  <configurationElements>
    <configurationSection name="ILuffyConfig" namespace="ILuffy.Core" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="iLuffyConfig">
      <elementProperties>
        <elementProperty name="Instances" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="instances" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/dee4848e-76d3-4a77-b8d8-e6706f84d49f/InstanceCollection" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationElement name="InstanceElement" namespace="ILuffy.Core">
      <attributeProperties>
        <attributeProperty name="Name" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="name" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/dee4848e-76d3-4a77-b8d8-e6706f84d49f/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Description" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="description" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/dee4848e-76d3-4a77-b8d8-e6706f84d49f/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="TypeFullName" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="typeFullName" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/dee4848e-76d3-4a77-b8d8-e6706f84d49f/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElementCollection name="InstanceCollection" namespace="ILuffy.Core" xmlItemName="instanceElement" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/dee4848e-76d3-4a77-b8d8-e6706f84d49f/InstanceElement" />
      </itemType>
    </configurationElementCollection>
  </configurationElements>
  <propertyValidators>
    <validators />
  </propertyValidators>
</configurationSectionModel>