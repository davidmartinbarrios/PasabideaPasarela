# bpmn-minimo (CDN)

Prueba mínima: 2 “procedimientos” (tareas BPMN) + 1 conector (SequenceFlow) con bpmn-js.

## Ejecutar
- Abre `index.html`.

Si algo se queja por abrir desde `file://`, usa un servidor local:

```bash
python -m http.server 8000
```

y abre:

http://localhost:8000

## Para tu caso Erwin Evolve
Lo más limpio: generar BPMN XML + BPMNDI (posiciones X/Y) y hacer `modeler.importXML(xml)`.
Así respetas exactamente las coordenadas del diagrama Erwin.
