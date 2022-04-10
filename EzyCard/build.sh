#export EZYFOX_SERVER_HOME=
mvn -pl . clean install
mvn -pl EzyCard-common -Pexport clean install
mvn -pl EzyCard-app-api -Pexport clean install
mvn -pl EzyCard-app-entry -Pexport clean install
mvn -pl EzyCard-plugin -Pexport clean install
cp EzyCard-zone-settings.xml $EZYFOX_SERVER_HOME/settings/zones/
