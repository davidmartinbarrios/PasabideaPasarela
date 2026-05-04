/* global BpmnJS */
(() => {
    window.addEventListener("message", async (event) => {
        try {
            if (!event.data || event.data.type !== "render-bpmn") {
                return;
            }

            if (typeof window.renderBpmn !== "function") {
                console.error("window.renderBpmn no está definido en bpmn/index.html");
                return;
            }

            await window.renderBpmn(event.data.xml);
        } catch (e) {
            console.error("Error procesando render-bpmn:", e);
        }
    });

    const modeler = new BpmnJS({
        container: '#canvas',
        keyboard: { bindTo: document }
    });

    async function createMinimalGraph() {
        await modeler.createDiagram();

        const canvas = modeler.get('canvas');
        const elementFactory = modeler.get('elementFactory');
        const modeling = modeler.get('modeling');
        const bpmnFactory = modeler.get('bpmnFactory');

        const root = canvas.getRootElement();

        const procA = elementFactory.createShape({ type: 'bpmn:Task' });
        const procB = elementFactory.createShape({ type: 'bpmn:Task' });

        const a = modeling.createShape(procA, { x: 220, y: 200 }, root);
        modeling.updateProperties(a, { name: 'Procedimiento A' });

        const b = modeling.createShape(procB, { x: 520, y: 200 }, root);
        modeling.updateProperties(b, { name: 'Procedimiento B' });

        const flowBo = bpmnFactory.create('bpmn:SequenceFlow', { name: 'Conector' });

        modeling.createConnection(
            a,
            b,
            { type: 'bpmn:SequenceFlow', businessObject: flowBo },
            root
        );

        canvas.zoom('fit-viewport');
    }

    async function exportXml() {
        const { xml } = await modeler.saveXML({ format: true });

        const panel = document.querySelector('.panel');
        const out = document.getElementById('xmlOut');

        out.value = xml;
        panel.style.display = 'flex';
        out.focus();
        out.select();
    }

    function wireUi() {
        document.getElementById('btnCreate').addEventListener('click', () => {
            createMinimalGraph().catch(console.error);
        });

        document.getElementById('btnExport').addEventListener('click', () => {
            exportXml().catch(console.error);
        });

        document.getElementById('btnClose').addEventListener('click', () => {
            document.querySelector('.panel').style.display = 'none';
        });
    }

    window.modeler = new BpmnJS({
        container: '#canvas'
    });

    window.renderBpmn = async function (xml) {
        try {
            await window.modeler.importXML(xml);
            const canvas = window.modeler.get("canvas");
            canvas.zoom("fit-viewport");
        } catch (err) {
            console.error("Error renderizando BPMN:", err);
        }
    };

    /*
    // --- ✅ API PARA .NET / WebView2 ---
    // Llama desde C#: window.renderBpmn(xmlString)
    window.renderBpmn = async function (xml) {
        try {
            if (!xml || typeof xml !== 'string') {
                console.warn('renderBpmn: xml vacío o no es string');
                return false;
            }

            // Limpia diagramas previos (evita residuos al re-renderizar)
            try {
                await modeler.clear();
            } catch (_) {
                // si tu versión de bpmn-js no tiene clear(), no pasa nada
            }

            // Importa el XML
            await modeler.importXML(xml);

            const canvas = modeler.get('canvas');
            canvas.zoom('fit-viewport');

            return true;
        } catch (e) {
            console.error('renderBpmn error:', e);

            // Para no dejar el visor en blanco, carga algo mínimo como fallback
            try {
                await createMinimalGraph();
            } catch (_) { }

            return false;
        }
    };*/

    // debug
    window.__bpmnModeler = modeler;

    wireUi();
    createMinimalGraph().catch(console.error);
})();


