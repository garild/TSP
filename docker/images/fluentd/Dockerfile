FROM fluent/fluentd
COPY fluent.conf /fluentd/etc/
RUN ["gem", "install", "fluent-plugin-elasticsearch", "--no-rdoc", "--no-ri", "--version", "1.9.7"]
RUN ["gem", "install", "fluent-plugin-http-healthcheck"]