apiVersion: v1
kind: ConfigMap
metadata:
  name: keycloak-keywind-theme
  namespace: {{ include "common.names.namespace" . | quote }}
  labels: {{- include "common.labels.standard" ( dict "customLabels" .Values.commonLabels "context" $ ) | nindent 4 }}
    app.kubernetes.io/component: keycloak
  {{- if .Values.commonAnnotations }}
  annotations: {{- include "common.tplvalues.render" ( dict "value" .Values.commonAnnotations "context" $ ) | nindent 4 }}
  {{- end }}
data:
  keywind.jar: |-
   {{ b64dec .Values.theme }} 