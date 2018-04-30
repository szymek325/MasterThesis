from sqlalchemy import create_engine
from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy.orm import sessionmaker
from dataLayer.configuration.connection_string_provider import ConnectionStringProvider

# get connection string
connection_string = ConnectionStringProvider().get_connection_string()

# create engine
engine = create_engine('mssql+pyodbc:///?odbc_connect={}'.format(connection_string))

# create a configured "Session" class
Session = sessionmaker(bind=engine)

# base for preparing entities
Base = declarative_base()
