<?xml version="1.0" encoding="utf-8"?>
<configurationSectionModel xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="03240baa-1538-4143-b65a-57538a5d390f" namespace="ILuffy.IOP.Configuration" xmlSchemaNamespace="urn:ILuffy.IOP.Configuration" xmlns="http://schemas.microsoft.com/dsltools/ConfigurationSectionDesigner">
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
    <configurationSection name="IOPConfigurationSection" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="iopConfigurationSection">
      <elementProperties>
        <elementProperty name="Logger" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="logger" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/03240baa-1538-4143-b65a-57538a5d390f/LoggerConfigurationElement" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationElement name="LoggerConfigurationElement">
      <attributeProperties>
        <attributeProperty name="FlushInterval" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="flushInterval" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/03240baa-1538-4143-b65a-57538a5d390f/Int32" />
          </type>
        </attributeProperty>
        <attributeProperty name="MaxLoggers" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="maxLoggers" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/03240baa-1538-4143-b65a-57538a5d390f/Int32" />
          </type>
        </attributeProperty>
        <attributeProperty name="IsMultipleThreadEnabled" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="isMultipleThreadEnabled" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/03240baa-1538-4143-b65a-57538a5d390f/Boolean" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
  </configurationElements>
  <propertyValidators>
    <validators />
  </propertyValidators>
</configurationSectionModel>