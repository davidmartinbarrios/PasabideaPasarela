/* global BpmnJS */
(() => {
    const modeler = new BpmnJS({
        container: '#canvas',
        keyboard: { bindTo: document }
    });

    // Exponer la instancia para depuración o llamadas externas
    window.modeler = modeler;

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
        const btnCreate = document.getElementById('btnCreate');
        const btnExport = document.getElementById('btnExport');
        const btnClose = document.getElementById('btnClose');

        if (btnCreate) {
            btnCreate.addEventListener('click', () => {
                createMinimalGraph().catch(console.error);
            });
        }

        if (btnExport) {
            btnExport.addEventListener('click', () => {
                exportXml().catch(console.error);
            });
        }

        if (btnClose) {
            btnClose.addEventListener('click', () => {
                const panel = document.querySelector('.panel');
                if (panel) panel.style.display = 'none';
            });
        }
    }

    // API pública para renderizar BPMN desde fuera
    window.renderBpmn = async function (xml) {
        try {
            if (!xml || typeof xml !== 'string') {
                console.warn('renderBpmn: xml vacío o no es string');
                return false;
            }

            await modeler.importXML(xml);

            const canvas = modeler.get('canvas');
            canvas.zoom('fit-viewport');

            return true;
        } catch (err) {
            console.error('Error renderizando BPMN:', err);
            return false;
        }
    };

    // Escucha mensajes enviados desde la página padre
    window.addEventListener('message', async (event) => {
        try {
            if (!event.data || event.data.type !== 'render-bpmn') {
                return;
            }

            await window.renderBpmn(event.data.xml);
        } catch (e) {
            console.error('Error procesando render-bpmn:', e);
        }
    });

    // debug
    window.__bpmnModeler = modeler;

    wireUi();
    createMinimalGraph().catch(console.error);
})();